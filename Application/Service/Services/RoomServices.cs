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
    public class RoomServices : IRoomServices
    {
        private readonly IBaseRepositories<Room> _baseRoomRepositories;
        private readonly Converter_Room _converter;
        private readonly ResponseObject<Response_Room> _respon;
        private readonly ResponseObject<List<Response_Room>> _responlist;
        private readonly Response_Pagination<Response_Room> _respon_pagination;

        public RoomServices(IBaseRepositories<Room> baseRoomRepositories, Converter_Room converter, ResponseObject<Response_Room> respon, ResponseObject<List<Response_Room>> responlist, Response_Pagination<Response_Room> respon_pagination)
        {
            _baseRoomRepositories = baseRoomRepositories;
            _converter = converter;
            _respon = respon;
            _responlist = responlist;
            _respon_pagination = respon_pagination;
        }

        public async Task<ResponseObject<Response_Room>> AddNewRoom(Request_Room request)
        {
            Room room = new Room();
            room.Name = request.Name;
            room.Capacity = request.Capacity;
            room.IsActive = true;
            room.Description = request.Description;
            room.Type = request.Type;
            room.Code = Guid.NewGuid().ToString();
            room.CinemaId = request.CinemaId;
            await _baseRoomRepositories.AddAsync(room);
            return _respon.ResponseSuccess("Add room Successfully", _converter.EntityToDTO(room));
        }

        public async Task<string> DeleteRoom(int id)
        {
            var roomDelete =await _baseRoomRepositories.FindAsync(id);
            if (roomDelete == null)
            {
                return "NotFound";
            }
            roomDelete.IsActive = false;
            await _baseRoomRepositories.UpdateAsync(roomDelete);
            return "Delete Seat Successfully";
        }

        public async Task<Response_Pagination<Response_Room>> GetAll(int pageNumber,int pageSize)
        {
            var list = await _baseRoomRepositories.GetAll();
            return _respon_pagination.ResponseSuccess("Danh sach Room",pageNumber, pageSize, _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<List<Response_Room>>> GetAllRoom()
        {
            var list = await _baseRoomRepositories.GetAll();
            return _responlist.ResponseSuccess("Danh sach Room", _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<Response_Room>> UpdateRoom(int id)
        {
            var roomUpdate = await _baseRoomRepositories.FindAsync(id);
            if (roomUpdate == null)
            {
                return _respon.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay cinema", null);
            }
            await _baseRoomRepositories.UpdateAsync(roomUpdate);
            return _respon.ResponseSuccess("Update Seat Successfully", _converter.EntityToDTO(roomUpdate));
        }
    }
}
