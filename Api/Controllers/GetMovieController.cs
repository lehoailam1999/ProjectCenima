using Application.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetMovieController : ControllerBase
    {
        private readonly IGetMovieServices _getMovieServices;

        public GetMovieController(IGetMovieServices getMovieServices)
        {
            _getMovieServices = getMovieServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetMovieByIdCinema(int idCinema)
        {
            var listMovie =await _getMovieServices.GetMovieByIdCinema(idCinema);
            return Ok(listMovie);
        }
    }
}
