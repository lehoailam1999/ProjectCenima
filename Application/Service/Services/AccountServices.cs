using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.AspNetCore.Http;
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

namespace Application.Service.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly AppDbContext _context;
        private readonly ResponseObject<Response_Resgister> _response;
        private readonly Converter_User _converter;
        private readonly IConfiguration _configuration;

        public AccountServices(AppDbContext context, Converter_User converter, IConfiguration configuration)
        {
            _context = context;
            _response = new ResponseObject<Response_Resgister>();
            _converter = converter;
            _configuration = configuration;
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
            if (user.Password != request.PassWord)
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

        public ResponseObject<Response_Resgister> Register(Request_Register request)
        {
            User user = new User();
            user.Name = request.Name;
            user.UserName = request.UserName;
            user.Point = request.Point;
            user.Password = request.Password;
            user.PhoneNumber = request.PhoneNumber; 
            user.Email = request.Email;
            user.IsActive = true;
            user.RoleId = 1;
            user.UserStatusId = 1;
            user.RankCustomerId = 1;
            _context.Users.Add(user);
            _context.SaveChanges();
            return _response.ResponseSuccess("Đăng ký tài khoản thành công", _converter.EntityToDTO(user));
        }
    }
}
