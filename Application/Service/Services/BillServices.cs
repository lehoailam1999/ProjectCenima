using Application.Payload.Converter.Converter_BillBook;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Services
{
    public class BillServices : IBillServices
    {
        private readonly IBaseRepositories<Bill> _baseRepositories;
        private readonly IBaseRepositories<BillTicket> _baseBillTicketRepositories;
        private readonly IBaseRepositories<BillFood> _baseBillFoodRepositories;
        private readonly IProjectRepositories _projectRepositories;
        private readonly ResponseObject<Response_Bill> _response;
        private readonly Converter_Bill _converter;

        public BillServices(IBaseRepositories<Bill> baseRepositories, IBaseRepositories<BillTicket> baseBillTicketRepositories, IBaseRepositories<BillFood> baseBillFoodRepositories, IProjectRepositories projectRepositories, ResponseObject<Response_Bill> response, Converter_Bill converter)
        {
            _baseRepositories = baseRepositories;
            _baseBillTicketRepositories = baseBillTicketRepositories;
            _baseBillFoodRepositories = baseBillFoodRepositories;
            _projectRepositories = projectRepositories;
            _response = response;
            _converter = converter;
        }

        public async Task<ResponseObject<Response_Bill>> AddNewBill(Request_Bill request)
        {
            Bill bill = new Bill();
            bill.ToTalDouble = 0;
            bill.Name = request.Name;
            bill.TradingCode = Guid.NewGuid().ToString();
            bill.CreateTime = DateTime.Now;
            bill.UpdateTime = DateTime.Now;
            bill.BillStatusId = 2;
            bill.PromotionId = 1;
            bill.UserId = request.UserId;
            bill.IsActive = false;
            bill.billTicket = null;
            bill.billFood = null;
            await _baseRepositories.AddAsync(bill);
            if (request.request_BillTickets!=null&&request.request_BillTickets.Any())
            {
                var lstBillTicket = await EntityToListBillTicket(bill.Id, request.request_BillTickets);
                if (lstBillTicket==null)
                {
                    return _response.ResponseError(StatusCodes.Status404NotFound, "Không thể thêm billticket.", null);
                }

                bill.billTicket = lstBillTicket;
                var lstBillTickets = await _projectRepositories.GetAllBillTicket(bill.Id);
                double totalTicket = lstBillTickets.Sum(x => x.Quantity * x.ticket.PriceTicket);
                //bill food
                var lstBillFood = await EntityToListBillFood(bill.Id, request.request_BillFoods);
                if (lstBillFood == null)
                {
                    return _response.ResponseError(StatusCodes.Status404NotFound, "Không thể thêm foodticket.", null);
                }

                bill.billFood = lstBillFood;
                var lstBillFoods = await _projectRepositories.GetAllBillFood(bill.Id);
                double totalFood = lstBillFoods.Sum(x => x.Quantity * x.food.Price);

                bill.ToTalDouble = totalTicket+ totalFood;

                await _baseRepositories.UpdateAsync(bill);
            }
            //billfood
           
            return _response.ResponseSuccess( "Them bill thành công.", _converter.EntityToDTO(bill));


        }
        public async Task<List<BillTicket>> EntityToListBillTicket(int idBill,List<Request_BillTickets> request)
        {
            var billTickets = await _baseBillTicketRepositories.SingleOrDefaultAsync(x => x.BillId == idBill);
            
            List<BillTicket> lst = new List<BillTicket>();
            foreach (var item in request)
            {
                BillTicket billTicket = new BillTicket();
                billTicket.BillId = idBill;
                billTicket.Quantity = request.Count;
                billTicket.TicketId = item.TicketId;
                lst.Add(billTicket);
            }
            await _baseBillTicketRepositories.AddRangeAsync(lst);
            return lst;

        }
        public async Task<List<BillFood>> EntityToListBillFood(int idBill, List<Request_BillFood> request)
        {
            List<BillFood> lst = new List<BillFood>();
            foreach (var item in request)
            {
                BillFood billTicket = new BillFood();
                billTicket.BillId = idBill;
                billTicket.Quantity = request.Count;
                billTicket.FoodId = item.FoodId;
                lst.Add(billTicket);
            }
            await _baseBillFoodRepositories.AddRangeAsync(lst);
            return lst;
        }


        public Task<string> DeleteBill(int id)
        {
            throw new NotImplementedException();
        }

        public Task<Response_Pagination<Response_Bill>> GetAll(int pageSize, int pageNumber)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseObject<Request_Bill>> UpdateFood(int id)
        {
            throw new NotImplementedException();
        }
    }
}
