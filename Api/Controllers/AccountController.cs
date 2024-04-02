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
        private readonly IEmailServices _serviceEmail;

        public AccountController(IAccountServices service, IEmailServices serviceEmail)
        {
            _service = service;
            _serviceEmail = serviceEmail;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register(Request_Register request_Register)
        {
            var register = await _service.Register(request_Register);
            return Ok(register);
        }
        [HttpPost("login")]
        public IActionResult Login(Request_Login request)
        {
            return Ok(_service.Login(request));
        }
        [HttpPost("ConfirmEmail")]
        public async Task<IActionResult> ConfirmEmail(string code)
        {
            try
            {
                var response = await _service.ConfirmEmail(code);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message); // Handle exceptions appropriately
            }
        }
    }
}
