using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BillController : ControllerBase
    {
        private readonly IBillServices _billServices;

        public BillController(IBillServices billServices)
        {
            _billServices = billServices;
        }
        [HttpPost]
        public async Task<IActionResult> AddBill(Request_Bill request)
        {
            var bill = await _billServices.AddNewBill(request);
            return Ok(bill);
        }
    }
}
