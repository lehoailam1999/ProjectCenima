﻿using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IFoodServices
    {
        Task<Response_Pagination<Response_Food>> GetAll(int pageSize,int pageNumber);
        Task<ResponseObject<Response_Food>> AddNewFood(Request_Food request);
        Task<string> DeleteFood(int id);
        Task<ResponseObject<Response_Food>> UpdateFood(int id);
    }
}
