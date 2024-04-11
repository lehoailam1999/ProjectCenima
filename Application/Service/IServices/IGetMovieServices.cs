using Application.Payload.DataRequest.InputRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IGetMovieServices
    {
        Task<ResponseObject<List<Response_Movie>>> GetMovieByIdCinema(int idCinema);
        Task<ResponseObject<List<Response_Movie>>> GetMovieByIdRoom(int idRoom);
        Task<ResponseObject<List<Response_Movie>>> GetMovieByHighLight(Input input);
        
    }
}
