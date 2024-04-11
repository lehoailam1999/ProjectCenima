using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
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
        private readonly Converter_Schedules _converter;
        private readonly ResponseObject<Response_Schedules> _respon;

        public SchedulesServices(IBaseRepositories<Schedule> baseSchedulesRepositories, Converter_Schedules converter, ResponseObject<Response_Schedules> respon)
        {
            _baseSchedulesRepositories = baseSchedulesRepositories;
            _converter = converter;
            _respon = respon;
        }

        public async Task<ResponseObject<Response_Schedules>> AddNewSchedules(Request_Schedules request)
        {
            Schedule schedule= new Schedule();
            schedule.StartAt = DateTime.Now;
            schedule.EndAt = DateTime.Now;
            schedule.Code = Guid.NewGuid().ToString();
            schedule.RoomId = request.RoomId;
            schedule.MovieId = request.MovieId;
            await _baseSchedulesRepositories.AddAsync(schedule);
            return _respon.ResponseSuccess("Add schedule Successfully", _converter.EntityToDTO(schedule));

        }

        public async Task<string> DeleteSchedules(int id)
        {
            var scheduleDelete =await _baseSchedulesRepositories.FindAsync(id);
            if (scheduleDelete == null)
            {
                return "Not Found";
            }
            await _baseSchedulesRepositories.DeleteAsync(scheduleDelete.Id);
            return "Delete Schedule Successfully";
        }

        public async Task<ResponseObject<List<Response_Schedules>>> GetAll()
        {
            ResponseObject<List<Response_Schedules>> listRes = new ResponseObject<List<Response_Schedules>>();
            var list = await _baseSchedulesRepositories.GetAll();
            return listRes.ResponseSuccess("Danh sach Schedule", _converter.EntityToListDTO(list));
        }

        public Task<ResponseObject<Response_Schedules>> UpdateSchedules(int id)
        {
            throw new NotImplementedException();
        }
    }
}
