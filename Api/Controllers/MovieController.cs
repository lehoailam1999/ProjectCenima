using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]

    public class MovieController : ControllerBase
    {
        private readonly IMovieServices _movieServices;

        public MovieController(IMovieServices movieServices)
        {
            _movieServices = movieServices;
        }
        [HttpGet]
        public async Task<IActionResult> GetAllMovie()
        {
            var listMovie = await _movieServices.GetAll();
            return Ok(listMovie);
        }
        [HttpPost]
        public async Task<IActionResult> AddNewMovie(Request_Movie request)
        {
            var Movie = await _movieServices.AddNewMovie(request);
            return Ok(Movie);
        }
        [HttpPut]
        public async Task<IActionResult> UpdateMovie(int id)
        {
            var Movie = await _movieServices.UpdateMovie(id);
            return Ok(Movie);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteMovie(int id)
        {
            var delete = await _movieServices.DeleteMovie(id);
            return Ok(delete);
        }
    }
}
