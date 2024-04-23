using Application.Payload.DataResponse;
using Application.Payload.Response;
using Azure;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IIncomeServices
    {
        Task<ResponseObject<List<Response_Revenue>>> GetIncome(DateTime startAt, DateTime endAt);
        Task<ResponseObject<List<Response_Food>>> GetFoodHighLight(int itemAmount);
    }
}
