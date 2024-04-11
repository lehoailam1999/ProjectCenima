using Application.Payload.DataResponse;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter.Converter_BillBook
{
    public class Converter_Bill
    {
        private readonly AppDbContext _context;
        private readonly IBaseRepositories<BillTicket> _baseRepositories;
        private readonly Convert_BillTickets _converter;
        private readonly Convert_BillFood _converterFood;

        public Converter_Bill(AppDbContext context, IBaseRepositories<BillTicket> baseRepositories, Convert_BillTickets converter, Convert_BillFood converterFood)
        {
            _context = context;
            _baseRepositories = baseRepositories;
            _converter = converter;
            _converterFood = converterFood;
        }

        public Response_Bill EntityToDTO(Bill bill)
        {
            Response_Bill response = new Response_Bill()
            {
                Name = bill.Name,
                ToTalDouble = bill.ToTalDouble,
                BillStatusId = bill.BillStatusId,
                PromotionId = bill.PromotionId,
                UserId = bill.UserId
            };
            if (bill.Id!=null)
            {
                var billTicket =_context.BillTickets.Where(x => x.BillId == bill.Id);
                if (billTicket!=null)
                {
                    response.response_BillTickets = billTicket.Select(bt => _converter.EntityToDTO(bt)).ToList();
                }
                var billFood = _context.BillFoods.Where(x => x.BillId == bill.Id);
                if (billFood != null)
                {
                    response.response_BillFoods = billFood.Select(bf => _converterFood.EntityToDTO(bf)).ToList();
                }
            }
            return response;
        }
    }
}
