using Application.Payload.DataRequest;
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
    public interface IMovieServices
    {
        Task<ResponseObject<List<Response_Movie>>> GetAll();
        Task<ResponseObject<Response_Movie>> AddNewMovie(Request_Movie request);
        Task<string> DeleteMovie(int id);
        Task<ResponseObject<Response_Movie>> UpdateMovie(int id, Request_Movie request);
    }
}
