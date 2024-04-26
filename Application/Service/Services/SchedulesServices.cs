using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Infrastructure.ImplementRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Services
{
    public class SchedulesServices : ISchedulesServices
    {
        private readonly IBaseRepositories<Schedule> _baseSchedulesRepositories;
        private readonly IBaseRepositories<Room> _baseRoomsRepositories;
        private readonly IBaseRepositories<Ticket> _baseTicketRepositories;
        private readonly IBaseRepositories<Seat> _baseSeatRepositories;
        private readonly Converter_Schedules _converter;
        private readonly ResponseObject<Response_Schedules> _respon;

        public SchedulesServices(IBaseRepositories<Schedule> baseSchedulesRepositories, IBaseRepositories<Room> baseRoomsRepositories, IBaseRepositories<Ticket> baseTicketRepositories, IBaseRepositories<Seat> baseSeatRepositories, Converter_Schedules converter, ResponseObject<Response_Schedules> respon)
        {
            _baseSchedulesRepositories = baseSchedulesRepositories;
            _baseRoomsRepositories = baseRoomsRepositories;
            _baseTicketRepositories = baseTicketRepositories;
            _baseSeatRepositories = baseSeatRepositories;
            _converter = converter;
            _respon = respon;
        }

        public async Task<ResponseObject<Response_Schedules>> AddNewSchedules(Request_Schedules request)
        {
            Schedule schedule= new Schedule();
            
            if (request.StartAt>request.EndAt)
            {
                return _respon.ResponseError(StatusCodes.Status400BadRequest, "Thoi gian bat dau phai nho hon thoi gian ket thuc", null);
            }
            var lstSchedulewithRooom = _baseSchedulesRepositories.Where(x => x.RoomId == request.RoomId ).ToList();

            var listCheckInroom = lstSchedulewithRooom.Where(x => x.StartAt <= request.EndAt && x.EndAt >= request.StartAt).ToList();
            if (listCheckInroom.Any())
            {
                return _respon.ResponseError(StatusCodes.Status400BadRequest, "Đã có phim  được chếu trong khoang thời gian nay", null);
            }
            var lstSchedule = _baseSchedulesRepositories.Where(x => x.RoomId == request.RoomId && x.MovieId == request.MovieId).ToList();

            var listCheck = lstSchedule.Where(x => x.StartAt <= request.EndAt && x.EndAt >= request.StartAt).ToList();
            if (listCheck.Any())
            {
                return _respon.ResponseError(StatusCodes.Status400BadRequest, "Phim này đã được chếu trong khoang thời gian nay", null);
            }
            schedule.StartAt = request.StartAt;
            schedule.EndAt = request.EndAt;
            schedule.Code = Guid.NewGuid().ToString();
            schedule.RoomId = request.RoomId;
            var room = await _baseRoomsRepositories.FindAsync(request.RoomId);
            if (room==null)
            {
                return _respon.ResponseError(StatusCodes.Status400BadRequest, "Cinema không có phòng này", null);

            }
            schedule.MovieId = request.MovieId;
            schedule.IsActive = true;
            schedule.ticket = null;
            await _baseSchedulesRepositories.AddAsync(schedule);
            if (request.request_Tickets != null && request.request_Tickets.Any())
            {
                // AddTicket
                var lstTicket = await EntityToListTicket(schedule.Id, request.request_Tickets);
                if (lstTicket == null)
                {
                    return _respon.ResponseError(StatusCodes.Status404NotFound, "Không thể thêm billticket.", null);
                }

                schedule.ticket = lstTicket;
            }
            await _baseSchedulesRepositories.UpdateAsync(schedule);
            return _respon.ResponseSuccess("Add schedule Successfully", _converter.EntityToDTO(schedule));
        }
        public async Task<List<Ticket>> EntityToListTicket(int scheduleId, List<Request_Ticket> request)
        {
            var tickets = await _baseTicketRepositories.SingleOrDefaultAsync(x => x.ScheduleId == scheduleId);

            List<Ticket> lst = new List<Ticket>();
            foreach (var item in request)
            {
                Ticket ticket = new Ticket();
                ticket.Code = Guid.NewGuid().ToString();
                ticket.ScheduleId = scheduleId;
                ticket.IsActive = true;
                ticket.PriceTicket = item.PriceTicket;
                ticket.SeatId = item.SeatId;
                var seat = await _baseSeatRepositories.FindAsync(item.SeatId);
                if (seat==null)
                {
                    return null;
                }
                ticket.SeatId = item.SeatId;
                lst.Add(ticket);
            }
            await _baseTicketRepositories.AddRangeAsync(lst);
            return lst;

        }
        public async Task<string> DeleteSchedules(int id)
        {
            var scheduleDelete =await _baseSchedulesRepositories.FindAsync(id);
            if (scheduleDelete == null)
            {
                return "Not Found schedule";
            }
            scheduleDelete.IsActive = false;
            await _baseSchedulesRepositories.UpdateAsync(scheduleDelete);
            return "Delete Schedule Successfully";
        }

        public async Task<ResponseObject<List<Response_Schedules>>> GetAll()
        {
            ResponseObject<List<Response_Schedules>> listRes = new ResponseObject<List<Response_Schedules>>();
            var list = await _baseSchedulesRepositories.GetAll();
            return listRes.ResponseSuccess("Danh sach Schedule", _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<Response_Schedules>> UpdateSchedules(int id)
        {
            var scheduleUpdate = await _baseSchedulesRepositories.FindAsync(id);
            if (scheduleUpdate == null)
            {
                return _respon.ResponseError(StatusCodes.Status404NotFound,"Không tìm thấy lịch",null);
            }
            await _baseSchedulesRepositories.UpdateAsync(scheduleUpdate);
            return _respon.ResponseSuccess( "Update thành công", _converter.EntityToDTO(scheduleUpdate));
        }

        public async Task<ResponseObject<List<Response_Schedules>>> GetAllById(int idMovie)
        { 
            ResponseObject<List<Response_Schedules>> listRes = new ResponseObject<List<Response_Schedules>>();

            var listWithIdMovie = await _baseSchedulesRepositories.GetAllByID(x=>x.MovieId==idMovie);
            return listRes.ResponseSuccess("Danh sach Schedule", _converter.EntityToListDTO(listWithIdMovie));

        }
    }
}
