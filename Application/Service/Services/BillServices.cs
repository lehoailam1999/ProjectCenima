using Application.Payload.Converter.Converter_BillBook;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
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
        private readonly IBaseRepositories<User> _baseUserRepositories;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;
        private readonly IBaseRepositories<BillTicket> _baseBillTicketRepositories;
        private readonly IBaseRepositories<Promotion> _basPromotiontRepositories;
        private readonly IBaseRepositories<BillFood> _baseBillFoodRepositories;
        private readonly IProjectRepositories _projectRepositories;
        private readonly ResponseObject<Response_Bill> _response;
        private readonly Converter_Bill _converter;
        private readonly IBaseRepositories<ConfirmEmail> _baseConfirmRepositories;
        private readonly IUserRepositories _userRepositories;

        public BillServices(IBaseRepositories<Bill> baseRepositories, IBaseRepositories<User> baseUserRepositories, IEmailServices emailServices, IConfiguration configuration, IBaseRepositories<BillTicket> baseBillTicketRepositories, IBaseRepositories<Promotion> basPromotiontRepositories, IBaseRepositories<BillFood> baseBillFoodRepositories, IProjectRepositories projectRepositories, ResponseObject<Response_Bill> response, Converter_Bill converter, IBaseRepositories<ConfirmEmail> baseConfirmRepositories, IUserRepositories userRepositories)
        {
            _baseRepositories = baseRepositories;
            _baseUserRepositories = baseUserRepositories;
            _emailServices = emailServices;
            _configuration = configuration;
            _baseBillTicketRepositories = baseBillTicketRepositories;
            _basPromotiontRepositories = basPromotiontRepositories;
            _baseBillFoodRepositories = baseBillFoodRepositories;
            _projectRepositories = projectRepositories;
            _response = response;
            _converter = converter;
            _baseConfirmRepositories = baseConfirmRepositories;
            _userRepositories = userRepositories;
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
            bill.IsActive = true;
            bill.billTicket = null;
            bill.billFood = null;
            await _baseRepositories.AddAsync(bill);
            if (request.request_BillTickets!=null&&request.request_BillTickets.Any())
            {
                // AddBillTicket
                var lstBillTicket = await EntityToListBillTicket(bill.Id, request.request_BillTickets);
                if (lstBillTicket==null)
                {
                    return _response.ResponseError(StatusCodes.Status404NotFound, "Không thể thêm billticket.", null);
                }

                bill.billTicket = lstBillTicket;
                var lstBillTickets = await _projectRepositories.GetAllBillTicket(bill.Id);
                double totalTicket = lstBillTickets.Sum(x => x.Quantity * x.ticket.PriceTicket);
                //AddBillFood
                var lstBillFood = await EntityToListBillFood(bill.Id, request.request_BillFoods);
                if (lstBillFood == null)
                {
                    return _response.ResponseError(StatusCodes.Status404NotFound, "Không thể thêm foodticket.", null);
                }

                bill.billFood = lstBillFood;
                var lstBillFoods = await _projectRepositories.GetAllBillFood(bill.Id);
                double totalFood = lstBillFoods.Sum(x => x.Quantity * x.food.Price);
                //promotion
                var promotion = await _basPromotiontRepositories.FindAsync(bill.PromotionId);
                if (promotion == null) 
                {
                   bill.ToTalDouble = totalTicket + totalFood;

                }
                else
                {
                    bill.ToTalDouble = (totalTicket + totalFood)*(promotion.Percent)/100;
                }


                await _baseRepositories.UpdateAsync(bill);
            }
           
            return _response.ResponseSuccess( "Thêm bil thành công.", _converter.EntityToDTO(bill));


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


        public async Task<string> DeleteBill(int id)
        {
            var deleteBill =await _baseRepositories.FindAsync(id);
            if (deleteBill==null)
            {
                return "Not Found";
            };
            deleteBill.IsActive = false;
            await _baseRepositories.UpdateAsync(deleteBill);
            return "Xóa bill thành công";
        }

        public async Task<Response_Pagination<Response_Bill>> GetAll(int pageNumber, int pageSize)
        {
            Response_Pagination<Response_Bill> response_Pagination = new Response_Pagination<Response_Bill>();
            var listBill= await _baseRepositories.GetAll();
            if (listBill==null && listBill.Count==0)
            {
                return response_Pagination.ResponseError(StatusCodes.Status404NotFound, "Phân trang không thành công");
            }
            return response_Pagination.ResponseSuccess("Danh sách :", pageNumber, pageSize, _converter.EntityToListDTO(listBill));
        }

        public Task<ResponseObject<Request_Bill>> UpdateFood(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ResponseObject<Response_Bill>> ThanhToan(int billId)
        {
            var bill = await _baseRepositories.FindAsync(billId);
            var user = await _baseUserRepositories.SingleOrDefaultAsync(x => x.Id == bill.UserId);

            var cofirmithUserId = await _userRepositories.GetConfirmEmailByUserId(user.Id);
            bool delete = await _baseConfirmRepositories.DeleteAsync(cofirmithUserId.Id);
            if (delete == true)
            {
                Random rand = new Random();
                int randomNumber = rand.Next(1000, 10000);

                string confirmationToken = randomNumber.ToString();
                ConfirmEmail confirm = new ConfirmEmail
                {
                    UserId = user.Id,
                    CodeActive = confirmationToken,
                    ExpiredTime = DateTime.UtcNow.AddHours(24),
                    IsConfirm = false
                };
                await _baseConfirmRepositories.AddAsync(confirm);
                string subject = "Thanh toán thành công";
                string body = "Thanh toán thành công";

                string emailResult = _emailServices.SendEmail(user.Email, subject, body);
                bill.BillStatusId = 1;
                await _baseRepositories.UpdateAsync(bill);

                return _response.ResponseSuccess($"Thanh toán thành công!{emailResult}", _converter.EntityToDTO(bill));
           }
            else
            {
                return _response.ResponseSuccess("Thanh toán không thành công thành công!CHưa gửi được email", null);
            }

        }

        public  async Task<Response_Bill> FindBill(int id)
        {
            var bill = await _baseRepositories.FindAsync(id);
            Response_Bill response_Bill = _converter.EntityToDTO(bill);
            return response_Bill;
        }
    }
}
