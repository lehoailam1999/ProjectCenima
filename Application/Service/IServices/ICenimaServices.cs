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
    public interface ICenimaServices
    {
        Task<Response_Pagination<Response_Cinema>> GetAll(int pageSize,int pageNumber);
        Task<ResponseObject<Response_Cinema>> AddNewCinema(Request_Cinema request);
        Task<string> DeleteCinema(int id);
        Task<ResponseObject<Response_Cinema>> UpdateCinema(int id);
    }
}
