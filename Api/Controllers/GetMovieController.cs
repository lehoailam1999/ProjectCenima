using Application.Payload.DataRequest.InputRequest;
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
        [HttpGet("GetMovieByIdCinema")]
        public async Task<IActionResult> GetMovieByIdCinema(int idCinema)
        {
            var listMovie =await _getMovieServices.GetMovieByIdCinema(idCinema);
            return Ok(listMovie);
        }
        [HttpGet("GetMovieByIdRoom")]
        public async Task<IActionResult> GetMovieByIdRoom(int idRoom)
        {
            var listMovie = await _getMovieServices.GetMovieByIdRoom(idRoom);
            return Ok(listMovie);
        }
        [HttpGet("GetMovieHighLight")]
        public async Task<IActionResult> GetMovieHighLight([FromQuery] Input input)
        {
            var listMovie = await _getMovieServices.GetMovieByHighLight(input);
            return Ok(listMovie);
        }
    }
}
