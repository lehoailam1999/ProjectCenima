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

        public async Task<IActionResult> GetAllCinema()
        {
            var listCinema =await _cenimaServices.GetAll();
            return Ok(listCinema);
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> AddNewCinema(Request_Cinema request)
        {
            var cinema =await _cenimaServices.AddNewCinema(request);
            return Ok(cinema);
        }
        [HttpPut]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> UpdateCinema(int id)
        {
            var cinema =await _cenimaServices.UpdateCinema(id);
            return Ok(cinema);
        }
        [HttpDelete]
        [Authorize(Roles = "Admin")]

        public async Task<IActionResult> DeleteCinema(int id)
        {
            var delete = await _cenimaServices.DeleteCinema(id);
            return Ok(delete);
        }
    }
}
