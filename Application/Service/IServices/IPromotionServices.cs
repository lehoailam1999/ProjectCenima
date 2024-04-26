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
    public interface IPromotionServices
    {

        Task<ResponseObject<List<Response_Promotion>>> GetAll();
        Task<ResponseObject<Response_Promotion>> AddNewPromotion(Request_Promotion request);
        Task<string> DeletePromotion(int id);
        Task<ResponseObject<Response_Promotion>> UpdatePromotion(int id,Request_Promotion request);
    }
}
