using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IUserRepositories
    {
        Task<User> GetUserByEmail(string email);
        Task<User> GetUserByUsername(string username);
        Task<User> GetUserByPhoneNumber(string phoneNumber);
        Task<ConfirmEmail> GetConfirmEmailById(int id);
        Task<ConfirmEmail> GetConfirmEmailByCode(string code);
        Task<ConfirmEmail> GetConfirmEmailByUserId(int userId);
        Task<ConfirmEmail> GetConfirmEmailByConFirmCode(string code);
    }
}
