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
    public class CinemaServices : ICenimaServices
    {
        private readonly IBaseRepositories<Cinema> _baseCinemaRepositories;
        private readonly Converter_Cinema _converter;
        private readonly ResponseObject<Response_Cinema> _respon;

        public CinemaServices(IBaseRepositories<Cinema> baseCinemaRepositories, ResponseObject<Response_Cinema> respon, Converter_Cinema converter)
        {
            _baseCinemaRepositories = baseCinemaRepositories;
            _respon = respon;
            _converter = converter;
        }

        public async Task<ResponseObject<Response_Cinema>> AddNewCinema(Request_Cinema request)
        {
            Cinema cinema = new Cinema();
            cinema.Address = request.Address;
            cinema.Code = request.Code;
            cinema.Description = request.Description;
            cinema.NameOfCinema = request.NameOfCinema;
            cinema.IsActive = true;
            await _baseCinemaRepositories.AddAsync(cinema);
            return _respon.ResponseSuccess("Add Cinema Successfully", _converter.EntityToDTO(cinema));

        }

        public async Task<string> DeleteCinema(int id)
        {
            var cinemaDelete = _baseCinemaRepositories.FindAsync(id);
            if (cinemaDelete == null)
            {
                return "NotFound";
            }
           await _baseCinemaRepositories.DeleteAsync(id);
            return "Delete Cinema Successfully";
              
           
        }

        public async Task<Response_Pagination<Response_Cinema>> GetAll(int pageSize,int pageNumber)
        {
            Response_Pagination < Response_Cinema> listRes = new Response_Pagination<Response_Cinema>();
            var list=await _baseCinemaRepositories.GetAll();
            if (list==null&& list.Count==0)
            {
                listRes.ResponseError(StatusCodes.Status404NotFound, "Danh sach cinema bị lỗi");
            }
            return listRes.ResponseSuccess("Danh sach Cinema",pageSize,pageNumber, _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<Response_Cinema>> UpdateCinema(int id)
        {
            var cinemaUpdate = await _baseCinemaRepositories.FindAsync(id);
            if (cinemaUpdate == null)
            {
                return _respon.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay cinema", null);
            }
            await _baseCinemaRepositories.UpdateAsync(cinemaUpdate);
            return _respon.ResponseSuccess( "Update Cinema Successfully", _converter.EntityToDTO(cinemaUpdate));


        }
    }
}
