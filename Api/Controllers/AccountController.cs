using Application.Constants;
using Application.Payload.DataRequest;
using Application.Service.IServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [Route(Constant.AppSettings.DEFAULT_CONTROLLER_ROUTE)]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IAccountServices _service;
        public AccountController(IAccountServices service)
        {
            _service = service;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register(Request_Register request_Register)
        {
            var register = await _service.Register(request_Register);
            return Ok(register);
        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login(Request_Login request)
        {
            return Ok(await _service.Login(request));
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
                return StatusCode(500, ex.Message);
            }
        }
        [HttpPut("ChangePassWord")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public async Task<IActionResult> ChagePassWord(Request_ChangePassword request)
        {
            int id = int.Parse(HttpContext.User.FindFirst("Id").Value);

            return Ok(await _service.ChangePassWord(id, request));
        }
        [HttpPost("ForgotPassword")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            return Ok(await _service.ForgotPassword(email));
        }
        [HttpPost("ReNewCodeToConfirm")]
        public async Task<IActionResult> ReNewCodeToConfirm(string email)
        {
            return Ok(await _service.ReNewCode(email));
        }
        [HttpPut("CreateNewPassWord")]
        public async Task<IActionResult> CreateNewPassWord(Request_NewPassWord request)
        {
            return Ok(await _service.ConfirmCreateNewPasWord(request));

        }
        [HttpGet]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> GetAll()
        {
            var list = await _service.GetAllUser();
            return Ok(list);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var delete = await _service.DeleteUser(id);
            return Ok(delete);
        }

    }
}
