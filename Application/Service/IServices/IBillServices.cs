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
    public interface IBillServices
    {
        Task<Response_Pagination<Response_Bill>> GetAll(int pageSize, int pageNumber);
        Task<ResponseObject<Response_Bill>> AddNewBill(Request_Bill request);
        Task<string> DeleteBill(int id);
        Task<ResponseObject<Request_Bill>> UpdateFood(int id);
    }
}
