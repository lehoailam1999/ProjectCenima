﻿using Application.Payload.DataRequest;
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
    public interface IAccountServices
    {
        Task<ResponseObject<Response_Resgister>> Register(Request_Register request);
        ResponseObject<Response_Token> Login(Request_Login request);
        Task<ResponseObject<ConfirmEmail>> ConfirmEmail(string code);
    }
}
