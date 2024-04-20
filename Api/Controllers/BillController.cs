using Application.Payload.DataRequest;
using Application.Service.IServices;
using Application.Service.Services;
using Domain.Entities;
using Domain.InterfaceRepositories;
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

        public BillController(IBillServices billServices, IBaseRepositories<Bill> baseBillRepositories, IVnPayService vnPayService)
        {
            _billServices = billServices;
            _baseBillRepositories = baseBillRepositories;
            _vnPayService = vnPayService;
        }

        [HttpPost]
        public async Task<IActionResult> AddBill(Request_Bill request)
        {
            var bill = await _billServices.AddNewBill(request);
            return Ok(bill);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllBill(int pageSize=1, int pageNumber=5)
        {
            var bill = await _billServices.GetAll(pageSize,pageNumber);
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
            var vnPayModel = new VnPaymentRequestModel
            {
                Amount = bill.ToTalDouble,
                CreatedDate = DateTime.Now,
                Description = $"{bill.Name}",
                FullName = bill.Name,
                OrderId = bill.Id
            };
            return Ok(_vnPayService.CreatePaymentUrl(HttpContext, vnPayModel));
        }
        [HttpGet("PaymentCallBack")]
        public async  Task<IActionResult> PaymentCallBack()
        {
            var response = _vnPayService.PaymentExecute(Request.Query);

            if (response == null || response.VnPayResponseCode != "00")
            {
                return BadRequest(new { message = $"Lỗi thanh toán VN Pay: {response.VnPayResponseCode}" });

            }
            if (!long.TryParse(response.OrderId, out long orderId))
            {
                return BadRequest(new { message = "Invalid OrderId" });
            }
            var bill = await _baseBillRepositories.SingleOrDefaultAsync(x=>x.Id==orderId);
            if (bill == null)
            {
                return NotFound(new { message = "Bill not found" });
            }

            bill.BillStatusId = 1;
            await _baseBillRepositories.UpdateAsync(bill);
            return Ok(new { status=StatusCodes.Status200OK,message = "Thanh toán VNPay thành công",data=bill });
        }
    }
}
