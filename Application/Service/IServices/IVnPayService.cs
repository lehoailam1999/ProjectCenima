using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IVnPayService
    {
        Task<string> CreatePaymentUrl(HttpContext context, VnPaymentRequestModel model,int billId);
        VnPaymentResponseModel PaymentExecute(IQueryCollection collections);  
    }
}
