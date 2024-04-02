using Application.Constants;
using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Domain.Validation;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BcryptNet = BCrypt.Net.BCrypt;

namespace Application.Service.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<Response_Resgister> _response;
        private readonly Converter_User _converter;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;
        private readonly IUserRepositories _userRepositories;

        private readonly IBaseRepositories<User> _baseRepositories;

        public AccountServices(IUserRepositories userRepositories,IBaseRepositories<User> baseRepositories,IEmailServices emailServices,AppDbContext context, Converter_User converter, IConfiguration configuration)
        {
            _context = context;
            _response = new ResponseObject<Response_Resgister>();
            _converter = converter;
            _configuration = configuration;
            _emailServices = emailServices;
            _baseRepositories = baseRepositories;
            _userRepositories = userRepositories;
        }

        public ResponseObject<Response_Token> Login(Request_Login request)
        {
            ResponseObject<Response_Token> response = new ResponseObject<Response_Token>();
            var user = _context.Users.SingleOrDefault(x => x.UserName == request.UserName);
            if (user == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Username không đúng ", null);
            }
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.PassWord))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin!", null);
            }
            var checkPassword = BcryptNet.Verify(request.PassWord, user.Password);

            if (!checkPassword)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác!", null);
            }
            return response.ResponseSuccess("Đăng nhập thành công!", GenerateAccessToken(user));
        }

        public string GennerateRefreshToKen()
        {
            var random = new byte[32];
            using(var item = RandomNumberGenerator.Create()) 
            {
                item.GetBytes(random);
                return Convert.ToBase64String(random);
            }
        }
        public Response_Token GenerateAccessToken(User user)
        {
            var jwtTokenHandle = new JwtSecurityTokenHandler();
            var secretKeyByte = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);
            var role = _context.Roles.SingleOrDefault(x => x.Id == user.RoleId);
            var tokenDescription = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim("Id", user.Id.ToString()),
                    /*new Claim("Email", user.Email),
                    new Claim("RoleId", role.Id.ToString()),*/
                    new Claim(ClaimTypes.Role, role.Code)
                }),
                Expires = DateTime.Now.AddMinutes(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha256Signature)

            };
            // Tạo jwt
            var token = jwtTokenHandle.CreateToken(tokenDescription);
            var accessToken = jwtTokenHandle.WriteToken(token);
            var refreshToken = GennerateRefreshToKen();
            RefreshToken rf = new RefreshToken()
            {
                Token = refreshToken,
                ExpiresTime = DateTime.Now.AddDays(1),
                UserId = user.Id
            };
            _context.RefreshTokens.Add(rf);
            _context.SaveChanges();
            Response_Token dataResponseToken = new Response_Token()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return dataResponseToken;
        }

        public  async Task<ResponseObject<Response_Resgister>> Register(Request_Register request)
        {
            User user = new User();
            user.Name = request.Name;
            var userWithUserName = _userRepositories.GetUserByUsername(request.UserName);
            if (userWithUserName!=null)
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "UserName already exists!!!!!",
                    Data = null
                };
            }
            user.UserName = request.UserName;
            user.Point = request.Point;
            //Check password
            if (!ValidateInput.IsValidPhoneNumber(request.Password))
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Password does not meet the required criteria.",
                    Data = null
                };
            }

            user.Password = BcryptNet.HashPassword(request.Password);
            var userWithPhone = _userRepositories.GetUserByPhoneNumber(request.PhoneNumber);
            if (userWithPhone != null)
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Phone Number already xists!!!!",
                    Data = null
                };
            }
            if (!ValidateInput.IsValidPhoneNumber(request.PhoneNumber))
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Phone Number contains 10 digits",
                    Data = null
                };
            }
            user.PhoneNumber = request.PhoneNumber;
            var userWithEmail = _userRepositories.GetUserByEmail(request.Email);
            if (userWithEmail != null)
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Email already exists!!!!!",
                    Data = null
                };
            }
            user.Email = request.Email;
            user.IsActive = false;
            user.RoleId = 1;
            user.UserStatusId = 1;
            user.RankCustomerId = 1;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            string confirmationToken = Guid.NewGuid().ToString();

            // Save confirmation token to database
            ConfirmEmail confirmEmail = new ConfirmEmail
            {
                UserId = user.Id,
                CodeActive = confirmationToken,
                ExpiredTime = DateTime.UtcNow.AddHours(24),
                IsConfirm = false
            };

            _context.ConfirmEmails.Add(confirmEmail);
            await _context.SaveChangesAsync();
            string subject = "Test";
            string body = "Hello World " + confirmationToken;
            // Send confirmation email
            string emailResult = _emailServices.SendEmail(user.Email, subject, body);
            

            return _response.ResponseSuccess($"Đăng ký tài khoản thành công. Vui lòng kiểm tra email để xác nhận đăng ký.{emailResult}", _converter.EntityToDTO(user));
        }
        public async Task<ResponseObject<ConfirmEmail>> ConfirmEmail(string code)
        {
            ResponseObject<ConfirmEmail> _response = new ResponseObject<ConfirmEmail>();

            var confirmEmail = await _context.ConfirmEmails.FirstOrDefaultAsync(x => x.CodeActive == code && x.ExpiredTime > DateTime.UtcNow);

            if (confirmEmail == null)
            {
                return _response.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không hợp lệ hoặc đã hết hạn", null) ;
            }

            var user = await _context.Users.FindAsync(confirmEmail.UserId);
            if (user == null)
            {
                return _response.ResponseError(StatusCodes.Status400BadRequest, "Người dùng không tồn tại", null);
            }

            user.IsActive = true;
            await _context.SaveChangesAsync();

            // Update ConfirmEmail
            confirmEmail.IsConfirm = true;
            await _context.SaveChangesAsync();

            return _response.ResponseSuccess("Xác nhận email thành công",null);
        }

        public async Task<string> ChangePassWord(int id, Request_ChangePassword request)
        {
            var user = await _context.Users.FindAsync(id);
            bool checkPassword = BcryptNet.Verify(request.OldPassword, user.Password);
            if (!checkPassword)
            {
                return "Incorrect password";
            }
            if (!request.NewPassword.Equals(request.ConfirmPassword))
            {
                return "Password do not macth";
            }
            user.Password = BcryptNet.HashPassword(request.NewPassword);
            await _baseRepositories.UpdateAsync(user);
            return "Change password success";
        }
    }
}
