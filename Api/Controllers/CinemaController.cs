using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CinemaController : ControllerBase
    {
        private readonly ICenimaServices _cenimaServices;

        public CinemaController(ICenimaServices cenimaServices)
        {
            _cenimaServices = cenimaServices;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllCinema(int pageSize=1,int pageNumber=5)
        {
            var listCinema =await _cenimaServices.GetAll(pageSize,pageNumber);
            return Ok(listCinema);
        }
        [HttpPost]

        public async Task<IActionResult> AddNewCinema(Request_Cinema request)
        {
            var cinema =await _cenimaServices.AddNewCinema(request);
            return Ok(cinema);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateCinema(int id)
        {
            var cinema =await _cenimaServices.UpdateCinema(id);
            return Ok(cinema);
        }
        [HttpDelete]

        public async Task<IActionResult> DeleteCinema(int id)
        {
            var delete = await _cenimaServices.DeleteCinema(id);
            return Ok(delete);
        }
    }
}
