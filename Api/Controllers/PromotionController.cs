using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PromotionController : ControllerBase
    {
        private readonly IPromotionServices _service;

        public PromotionController(IPromotionServices service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllFood()
        {
            var listPromotion = await _service.GetAll();
            return Ok(listPromotion);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewFood(Request_Promotion request)
        {
            var promotion = await _service.AddNewPromotion(request);
            return Ok(promotion);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateFood(int id, Request_Promotion request)
        {
            var promotion = await _service.UpdatePromotion(id,request);
            return Ok(promotion);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteFood(int id)
        {
            var delete = await _service.DeletePromotion(id);
            return Ok(delete);
        }
    }
}
