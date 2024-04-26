using Application.Payload.DataRequest;
using Application.Service.IServices;
using Application.Service.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FoodController : ControllerBase
    {
        private readonly IFoodServices _foodServices;

        public FoodController(IFoodServices foodServices)
        {
            _foodServices = foodServices;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> GetAllFood(int pageNumber = 1, int pageSize = 5)
        {
            var listFood = await _foodServices.GetAll(pageNumber, pageSize);
            return Ok(listFood);
        }
       
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddNewFood(Request_Food request)
        {
            var food = await _foodServices.AddNewFood(request);
            return Ok(food);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateFood(int id)
        {
            var food = await _foodServices.UpdateFood(id);
            return Ok(food);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteFood(int id)
        {
            var delete = await _foodServices.DeleteFood(id);
            return Ok(delete);
        }
    }
}
