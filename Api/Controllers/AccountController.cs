using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _service;

        public AccountController(IAccountServices service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public IActionResult Register(Request_Register request_Register)
        {
            return Ok(_service.Register(request_Register));
        }
        [HttpPost("login")]
        public IActionResult Login(Request_Login request)
        {
            return Ok(_service.Login(request));
        }
    }
}
