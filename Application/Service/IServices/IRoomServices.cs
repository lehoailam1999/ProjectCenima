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
        Task<ResponseObject<List<Response_Room>>> GetAll();
        Task<ResponseObject<Response_Room>> AddNewRoom(Request_Room request);
        Task<string> DeleteRoom(int id);
        Task<ResponseObject<Response_Room>> UpdateRoom(int id);
    }
}
