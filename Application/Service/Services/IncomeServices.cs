using Application.Payload.Converter;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Azure;
using Domain.Entities;
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
        private readonly ResponseObject<List<Response_Revenue>> _response;
        private readonly ResponseObject<List<Response_Food>> _responseFood;
        private readonly Converter_Food _convert;

        public IncomeServices(IIncomeRepositories incomeRepo, ResponseObject<List<Response_Revenue>> response, ResponseObject<List<Response_Food>> responseFood, Converter_Food convert)
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

        public async Task<ResponseObject<List<Response_Revenue>>> GetIncome(DateTime startAt, DateTime endAt)
        {
            var listCinema = await _incomeRepo.GetIncome();
            var listCinemaWwith = listCinema
                .Where(b => b.CreateTime >= startAt && b.CreateTime <= endAt && b.BillStatusId == 1)
                .GroupBy(b => b.billTicket.FirstOrDefault().ticket.schedule.room.cinema.Id)
                .Select(g => new Response_Revenue
                {
                    Id = g.Key,
                    NameOfCinema = g.First().billTicket.FirstOrDefault().ticket.schedule.room.cinema.NameOfCinema,
                    Address = g.First().billTicket.FirstOrDefault().ticket.schedule.room.cinema.Address,
                    Description = g.First().billTicket.FirstOrDefault().ticket.schedule.room.cinema.Description,
                    ToTalRevenue = g.Sum(b => b.ToTalDouble),
                    Movies = g.SelectMany(b => b.billTicket)
                      .Where(bt => bt.ticket?.schedule?.movie != null)
                       .GroupBy(bt => bt.ticket.schedule.movie.Id)
                      .Select(m => new MovieInfo
                      {
                          MovieId = m.First().ticket.schedule.movie.Id,
                          MovieName = m.First().ticket.schedule.movie.Name,
                          ToTalRevenueWithMovie = m.Sum(bt => bt.bill?.ToTalDouble ?? 0)
                      }).ToList()
                }).ToList();
            if (listCinemaWwith==null&&listCinema.Count==0)
            {
                return _response.ResponseError(StatusCodes.Status404NotFound,"Lỗi phần doanh thu Cinema", null);

            }
            return _response.ResponseSuccess("Doanh thu Cinema", listCinemaWwith);
        }    
}
}
