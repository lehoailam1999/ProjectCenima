using Application.Payload.Converter.Converter_BillBook;
using Application.Payload.DataRequest;
using Application.Service.IServices;
using Application.Service.Services;
using Azure.Core;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillServices _billServices;
        private readonly IBaseRepositories<Bill> _baseBillRepositories;
        private readonly IVnPayService _vnPayService;
        private readonly Converter_Bill _converter;

        public BillController(IBillServices billServices, IBaseRepositories<Bill> baseBillRepositories, IVnPayService vnPayService, Converter_Bill converter)
        {
            _billServices = billServices;
            _baseBillRepositories = baseBillRepositories;
            _vnPayService = vnPayService;
            _converter = converter;
        }

        [HttpPost]
        public async Task<IActionResult> AddBill(Request_Bill request)
        {
            var bill = await _billServices.AddNewBill(request);
            return Ok(bill);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBill(int pageNumber=1, int pageSize =5)
        {
            var bill = await _billServices.GetAll(pageNumber,pageSize);
            return Ok(bill);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteBill(int id)
        {
            var bill = await _billServices.DeleteBill(id);
            return Ok(bill);
        }
        [HttpPost("ThanhToan")]
        public async Task<IActionResult> ThanhToan(int id)
        {
            var bill = await _billServices.ThanhToan(id);
            return Ok(bill);
        }
        [HttpPost("VNPay")]
        public async Task<IActionResult> ThanhToanVNpay(int id)
        {
            var bill = await _baseBillRepositories.FindAsync(id);
            if (bill.BillStatusId == 1)
            {
                return NotFound(new { message = "This bill has already been paid." });

            }
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = bill.ToTalDouble,
                CreatedDate = DateTime.Now,
                Description = $"{bill.Name}",
                FullName = bill.Name,
                OrderId = bill.Id.ToString()
            };
            return Ok(new { status=StatusCodes.Status200OK,message="Moi ban thanh toan online",url= await _vnPayService.CreatePaymentUrl(HttpContext, vnPayModel,id) });
        }
        [HttpGet("PaymentCallBack")]
        public async Task<IActionResult> PaymentCallBack()
        {
            var response =  _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            { 
                return BadRequest(new {status=StatusCodes.Status400BadRequest, message = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}" });

            }      
            var bill = await _baseBillRepositories.SingleOrDefaultAsync(x => x.Id==int.Parse(response.OrderId));
            if (bill == null)
            {
                return NotFound(new { message = "Bill not found" });
            }
           
            bill.BillStatusId = 1;
            await _baseBillRepositories.UpdateAsync(bill);
            return Ok(new { status = StatusCodes.Status200OK, message = "Thanh toán VNPay thành công", data = _converter.EntityToDTO(bill)});
        }
    }
}
