using Application.Constants;
using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
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
        private readonly ResponseObject<Response_Resgister> _response;
        private readonly Converter_User _converter;
        private readonly IEmailServices _emailServices;
        private readonly IConfiguration _configuration;
        private readonly IUserRepositories _userRepositories;

        private readonly IBaseRepositories<User> _baseRepositories;
        private readonly IBaseRepositories<Role> _baseRolesRepositories;
        private readonly IBaseRepositories<RefreshToken> _baseRefreshRepositories;
        private readonly IBaseRepositories<ConfirmEmail> _baseConfirmRepositories;
        public AccountServices(IBaseRepositories<Role> baseRolesRepositories,
            IBaseRepositories<RefreshToken> baseRefreshRepositories,
            IBaseRepositories<ConfirmEmail> baseConfirmRepositories,
            IUserRepositories userRepositories,
            IBaseRepositories<User> baseRepositories,
            IEmailServices emailServices, 
            Converter_User converter, 
            IConfiguration configuration)
        {
            _response = new ResponseObject<Response_Resgister>();
            _converter = converter;
            _configuration = configuration;
            _emailServices = emailServices;
            _baseRepositories = baseRepositories;
            _userRepositories = userRepositories;
            _baseConfirmRepositories = baseConfirmRepositories;
            _baseRefreshRepositories = baseRefreshRepositories;
            _baseRolesRepositories = baseRolesRepositories;
        }
        public async Task<ResponseObject<Response_Token>> Login(Request_Login request)
        {
            ResponseObject<Response_Token> response = new ResponseObject<Response_Token>();
           var user = await _userRepositories.GetUserByUsername(request.UserName);
            if (user == null)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Username không đúng ", null);
            }
            if (string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.PassWord))
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Vui lòng điền đầy đủ thông tin!", null);
            }
            if (user.UserStatusId == 2)
            {
                return response.ResponseError(StatusCodes.Status404NotFound, "Tài khoản chưa được xác thực Email", null);

            }
            var checkPassword = BcryptNet.Verify(request.PassWord, user.Password);

            if (!checkPassword)
            {
                return response.ResponseError(StatusCodes.Status400BadRequest, "Mật khẩu không chính xác!", null);
            }
            return response.ResponseSuccess("Đăng nhập thành công!",await GenerateAccessToken(user));
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
        public async Task<Response_Token> GenerateAccessToken(User user)
        {
            var jwtTokenHandle = new JwtSecurityTokenHandler();
            var secretKeyByte = System.Text.Encoding.UTF8.GetBytes(_configuration.GetSection("AppSettings:SecretKey").Value);
            var role = await _baseRolesRepositories.FindAsync(user.RoleId);
            if (role == null)
            {
                throw new Exception("Role not found for the user.");
            }
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
            await _baseRefreshRepositories.AddAsync(rf);
            Response_Token dataResponseToken = new Response_Token()
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            };
            return dataResponseToken;
        }

        public async Task<ResponseObject<Response_Resgister>> Register(Request_Register request)
        {
            User user = new User();
            user.Name = request.Name;
            var userWithUserName = await _userRepositories.GetUserByUsername(request.UserName);
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
           /* if (!ValidateInput.IsValidPassword(request.Password))
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Password does not meet the required criteria.",
                    Data = null
                };
            }*/

            user.Password = BcryptNet.HashPassword(request.Password);
            var userWithPhone =await _userRepositories.GetUserByPhoneNumber(request.PhoneNumber);
            if (userWithPhone != null)
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Phone Number already xists!!!!",
                    Data = null
                };
            }
            /*if (!ValidateInput.IsValidPhoneNumber(request.PhoneNumber))
            {
                return new ResponseObject<Response_Resgister>
                {
                    Status = StatusCodes.Status400BadRequest,
                    Message = "Phone Number contains 10 digits",
                    Data = null
                };
            }*/
            user.PhoneNumber = request.PhoneNumber;
            var userWithEmail = await _userRepositories.GetUserByEmail(request.Email);
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
            user.IsActive = true;
            user.RoleId = 1;
            user.UserStatusId = 2;//
            user.RankCustomerId = 1;
            await _baseRepositories.AddAsync(user);
            /*_context.Users.Add(user);
            await _context.SaveChangesAsync();*/
            Random rand = new Random();
            int randomNumber = rand.Next(1000, 10000);

            string confirmationToken = randomNumber.ToString();

            ConfirmEmail confirmEmail = new ConfirmEmail
            {
                UserId = user.Id,
                CodeActive = confirmationToken,
                ExpiredTime = DateTime.UtcNow.AddHours(24),
                IsConfirm = false
            };

            /*            _context.ConfirmEmails.Add(confirmEmail);
            */
            await _baseConfirmRepositories.AddAsync(confirmEmail);
            string subject = "Test";
            string body = "Xin chào :" + confirmationToken;
            string emailResult = _emailServices.SendEmail(user.Email, subject, body);
            return _response.ResponseSuccess($"Đăng ký tài khoản thành công. Vui lòng kiểm tra email để xác nhận đăng ký.{emailResult}",await _converter.EntityToDTO(user));
        }
        public async Task<ResponseObject<ConfirmEmail>> ConfirmEmail(string code)
        {
            ResponseObject<ConfirmEmail> _response = new ResponseObject<ConfirmEmail>();
            var confirmEmail = await _userRepositories.GetConfirmEmailByCode(code);

            if (confirmEmail == null )
            {
                return _response.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận không hợp lệ ", null) ;
            }
            if (confirmEmail.ExpiredTime <= DateTime.UtcNow)
            {
                return _response.ResponseError(StatusCodes.Status400BadRequest, "Mã xác nhận đã hết hạn", null);
            }

            var user = await _baseRepositories.FindAsync(confirmEmail.UserId);
            if (user == null)
            {
                return _response.ResponseError(StatusCodes.Status400BadRequest, "Người dùng không tồn tại", null);
            }

            user.UserStatusId = 1;
            await _baseRepositories.UpdateAsync(user);

            // Update ConfirmEmail
            confirmEmail.IsConfirm = true;
            await _baseConfirmRepositories.UpdateAsync(confirmEmail);

            return _response.ResponseSuccess("Xác nhận email thành công",null);
        }

        public async Task<string> ReNewCode(string email)
        {
            var userWithEmail = await _userRepositories.GetUserByEmail(email);
            if (userWithEmail == null)
            {
                return "Email don't exists!!!!";
            }
            var cofirmithUserId = await _userRepositories.GetConfirmEmailByUserId(userWithEmail.Id);
            bool delete = await _baseConfirmRepositories.DeleteAsync(cofirmithUserId.Id);
            if (delete == true)
            {
                Random rand = new Random();
                int randomNumber = rand.Next(1000, 10000);

                string confirmationToken = randomNumber.ToString();
                ConfirmEmail confirm = new ConfirmEmail
                {
                    UserId = userWithEmail.Id,
                    CodeActive = confirmationToken,
                    ExpiredTime = DateTime.UtcNow.AddHours(24),
                    IsConfirm = false
                };
                await _baseConfirmRepositories.AddAsync(confirm);
                string subject = "Renew Password";
                string body = "Mã xác nhận là :" + confirmationToken;
                string emailResult = _emailServices.SendEmail(email, subject, body);
                return $"{emailResult}! Mời bạn kiểm tra Email";
            }
            else
            {
                return "Bạn chưa gửi được mã xác nhận";
            }
        }
        public async Task<string> ChangePassWord(int id, Request_ChangePassword request)
        {
            var user = await _baseRepositories.FindAsync(id);
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

        public async Task<string> ForgotPassword(string email)
        {
           var userWithEmail = await _userRepositories.GetUserByEmail(email);
            if (userWithEmail==null)
            {
                return "Email don't exists!!!!";
            }
            var cofirmithUserId = await _userRepositories.GetConfirmEmailByUserId(userWithEmail.Id);
            bool delete = await _baseConfirmRepositories.DeleteAsync(cofirmithUserId.Id);
            if (delete ==true)
            {
                Random rand = new Random();
                int randomNumber = rand.Next(1000, 10000);
                string confirmationToken = randomNumber.ToString();
                ConfirmEmail confirm = new ConfirmEmail
                {
                    UserId = userWithEmail.Id,
                    CodeActive = confirmationToken,
                    ExpiredTime = DateTime.UtcNow.AddHours(24),
                    IsConfirm = false
                };
                await _baseConfirmRepositories.AddAsync(confirm);
                string subject = "Forgot Password";
                string body = "Mã xác nhận là :" + confirmationToken;
                string emailResult = _emailServices.SendEmail(email, subject, body);
                return $"{emailResult}! Mời bạn kiểm tra Email";
            }
            else
            {
                return "Bạn chưa gửi được mã xác nhận";
            }
        }

        public async Task<string> ConfirmCreateNewPasWord(Request_NewPassWord request)
        {
            try
            {
                var confiremEmail = await _userRepositories.GetConfirmEmailByConFirmCode(request.ConfirmCode);
                if (confiremEmail == null)
                {
                    return "Mã xác nhận không đúng";
                }
                if (!request.Password.Equals(request.ConfirmPassword))
                {
                    return "Password don't match";
                }
                var user = await _baseRepositories.FindAsync(confiremEmail.UserId);
                user.Password = BcryptNet.HashPassword(request.Password);
                await _baseRepositories.UpdateAsync(user);
                confiremEmail.IsConfirm = true;
                await _baseConfirmRepositories.UpdateAsync(confiremEmail); 
                return "Create password successfully";
            }
            catch (Exception ex)
            {

                return "Error: " + ex.Message;
            }


        }

        public async Task<IEnumerable<User>> GetAllUser()
        {
            var list =await _baseRepositories.GetAll();
            return list;
        }

        public async Task<string> DeleteUser(int id)
        {
/*            var user = await _baseRepositories.FindAsync(id);
*/            bool delete = await _baseRepositories.DeleteAsync(id);
            if (delete)
            {
                return "Xóa user thành công";
            }
            else
            {
                return "Xóa User thất bại";
            }
        }
    }
}
