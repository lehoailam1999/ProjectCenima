using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Services
{
    public class SeatServices : ISeatServices
    {
        private readonly IBaseRepositories<Seat> _baseSeatRepositories;
        private readonly Converter_Seat _converter;
        private readonly ResponseObject<Response_Seat> _respon;

        public SeatServices(IBaseRepositories<Seat> baseSeatRepositories, Converter_Seat converter, ResponseObject<Response_Seat> respon)
        {
            _baseSeatRepositories = baseSeatRepositories;
            _converter = converter;
            _respon = respon;
        }

        public async Task<ResponseObject<Response_Seat>> AddNewSeat(Request_Seat request)
        {
            Seat seat = new Seat();
            seat.Number = request.Number;
            seat.Line = request.Line;
            seat.IsActive = true;
            seat.RoomId = request.RoomId;
            seat.SeatTypeId = request.SeatTypeId;
            seat.SeatStatusId = request.SeatStatusId;
            await _baseSeatRepositories.AddAsync(seat);
            return _respon.ResponseSuccess("Add Seat Successfully", _converter.EntityToDTO(seat));

        }

        public async Task<string> DeleteSeat(int id)
        {
            var seatDelete = _baseSeatRepositories.FindAsync(id);
            if (seatDelete == null)
            {
                return "NotFound";
            }
            await _baseSeatRepositories.DeleteAsync(seatDelete.Id);
            return "Delete Seat Successfully";
        }

        public async Task<ResponseObject<List<Response_Seat>>> GetAll()
        {
            ResponseObject<List<Response_Seat>> listRes = new ResponseObject<List<Response_Seat>>();
            var list = await _baseSeatRepositories.GetAll();
            return listRes.ResponseSuccess("Danh sach Cinema",  _converter.EntityToListDTO(list));
        }
        public async Task<ResponseObject<Response_Seat>> UpdateSeat(int id)
        {
            var seatUpdate = await _baseSeatRepositories.FindAsync(id);
            if (seatUpdate == null)
            {
                return _respon.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay cinema", null);
            }
            await _baseSeatRepositories.UpdateAsync(seatUpdate);
            return _respon.ResponseSuccess("Update Seat Successfully",  _converter.EntityToDTO(seatUpdate));

        }
    }
}
