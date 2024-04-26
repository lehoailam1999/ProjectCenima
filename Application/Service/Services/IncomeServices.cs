using Application.Payload.Converter;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Azure;
using Domain.Entities;
using Domain.Enumerates;
using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Services
{
    public class IncomeServices : IIncomeServices
    {
        private readonly IIncomeRepositories _incomeRepo;
        private readonly ResponseObject<List<CinemaRevenue>> _response;
        private readonly ResponseObject<List<Response_Food>> _responseFood;
        private readonly Converter_Food _convert;

        public IncomeServices(IIncomeRepositories incomeRepo, ResponseObject<List<CinemaRevenue>> response, ResponseObject<List<Response_Food>> responseFood, Converter_Food convert)
        {
            _incomeRepo = incomeRepo;
            _response = response;
            _responseFood = responseFood;
            _convert = convert;
        }

        public async Task<ResponseObject<List<Response_Food>>> GetFoodHighLight(int itemAmount)
        {
            var listFood = await _incomeRepo.GetFoodHighLight(itemAmount);
            if (listFood==null&&listFood.Count==0)
            {
                return _responseFood.ResponseError(StatusCodes.Status404NotFound,"Danh sách đồ ăn nổi bật nhất bị lỗi", null);

            }
            return _responseFood.ResponseSuccess("Danh sách đồ ăn nổi bật nhất", _convert.EntityToListDTO(listFood));
        }

        public async Task<ResponseObject<List<CinemaRevenue>>> GetIncome(DateTime startAt, DateTime endAt)
        {
            var listCinemaWwith = await _incomeRepo.GetIncome(startAt,endAt);
          
            if (listCinemaWwith==null&& listCinemaWwith.Count==0)
            {
                return _response.ResponseError(StatusCodes.Status404NotFound,"Lỗi phần doanh thu Cinema", null);

            }
            return _response.ResponseSuccess("Doanh thu Cinema", listCinemaWwith);
        }    
    }
}
