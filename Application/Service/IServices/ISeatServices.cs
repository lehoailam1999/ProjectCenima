using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface ISeatServices
    {
        Task<Response_Pagination<Response_Seat>> GetAll(int pageSize,int pageNumber);
        Task<ResponseObject<Response_Seat>> AddNewSeat(Request_Seat request);
        Task<string> DeleteSeat(int id);
        Task<ResponseObject<Response_Seat>> UpdateSeat(int id);
        Task<ResponseObject<List<Response_Seat>>> GetAllInRoom(int idRoom);

    }
}
