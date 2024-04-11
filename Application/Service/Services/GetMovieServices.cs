using Application.Payload.Converter;
using Application.Payload.DataRequest.InputRequest;
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
    public class GetMovieServices : IGetMovieServices
    {
        private readonly IGetMovieRepositories _getMovieRepositories;
        private readonly Converter_Movie _converter;
        public GetMovieServices(IGetMovieRepositories getMovieRepositories, Converter_Movie converter)
        {
            _getMovieRepositories = getMovieRepositories;
            _converter = converter;
        }

        public async Task<ResponseObject<List<Response_Movie>>> GetMovieByHighLight(Input input)
        {
            ResponseObject<List<Response_Movie>> responseObject = new ResponseObject<List<Response_Movie>>();
            var list = await _getMovieRepositories.GetMovieGighLight();
            if (list == null)
            {
                return responseObject.ResponseError(StatusCodes.Status404NotFound, "Khong co  phim nổi bật: ", null);
            }
            if (input.StartAt.HasValue  && input.EndAt.HasValue&&input.TotalMovieHighLight.HasValue)
            {
                var listWith = list.Select(m => new
                {
                    Movie = m,
                    TotalTicketsSold = m.schedule?.Sum(s => s.ticket?.Sum(t => t.billTickets?.Sum(bt => bt.Quantity)))
                })
                .OrderByDescending(m => m.TotalTicketsSold)
                .Take(input.TotalMovieHighLight ?? 0)
                .Select(m => m.Movie)
                .Where(x=>x.PremiereDate>=input.StartAt&&x.PremiereDate<=input.EndAt).ToList();
                if (listWith==null && listWith.Count==0)
                {
                    return responseObject.ResponseError(StatusCodes.Status404NotFound,"Danh sách phim nổi bật: ",null);
                }
                return responseObject.ResponseSuccess("Danh sách phim nổi bật: ", _converter.EntityToListDTO(listWith));
            }
            if (input.TotalMovieHighLight.HasValue)
            {
                var listWith = list.Select(m => new
                {
                    Movie = m,
                    TotalTicketsSold = m.schedule?.Sum(s => s.ticket?.Sum(t => t.billTickets?.Sum(bt => bt.Quantity)))
                })
                .OrderByDescending(m => m.TotalTicketsSold)
                .Take(input.TotalMovieHighLight ?? 0)
                .Select(m => m.Movie)
                .ToList();
                if (listWith == null && listWith.Count == 0)
                {
                    return responseObject.ResponseError(StatusCodes.Status404NotFound, "Danh sách phim nổi bật: ", null);
                }
                return responseObject.ResponseSuccess("Danh sách phim nổi bật: ", _converter.EntityToListDTO(listWith));
            }
           
            return responseObject.ResponseSuccess("Danh sách phim nổi bật: ", _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<List<Response_Movie>>> GetMovieByIdCinema(int idCinema)
        {
            ResponseObject<List<Response_Movie>> responseObject = new ResponseObject<List<Response_Movie>>(); 
            var list= await _getMovieRepositories.GetMovieByIdCinema(idCinema);
            if (list == null)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Danh sách phim theo rạp", null);

            }
            return responseObject.ResponseSuccess("Danh sách phim theo rạp",_converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<List<Response_Movie>>> GetMovieByIdRoom(int idRoom)
        {
            ResponseObject<List<Response_Movie>> responseObject = new ResponseObject<List<Response_Movie>>();
            var list = await _getMovieRepositories.GetMovieByIdRoom(idRoom);
            if (list == null)
            {
                return responseObject.ResponseError(StatusCodes.Status400BadRequest, "Danh sách phim theo room", null);

            }
            return responseObject.ResponseSuccess("Danh sách phim theo room", _converter.EntityToListDTO(list));
        }
    }
}
