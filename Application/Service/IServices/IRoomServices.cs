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
    public interface IRoomServices
    {
        Task<Response_Pagination<Response_Room>> GetAll(int pageSize, int pageNumber);
        Task<ResponseObject<List<Response_Room>>> GetAllRoom();
        Task<ResponseObject<Response_Room>> AddNewRoom(Request_Room request);
        Task<string> DeleteRoom(int id);
        Task<ResponseObject<Response_Room>> UpdateRoom(int id);
    }
}
