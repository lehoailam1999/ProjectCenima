using Domain.Entities;
using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplementRepositories
{
    public class UserRepositories:IUserRepositories
    {
        private readonly AppDbContext _context;

        public UserRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User> GetUserByEmail(string email)
        {
           var user = await _context.Users.SingleOrDefaultAsync(x=>x.Email.ToLower().Equals(email.ToLower()));
            return user;
        }

        public async Task<User> GetUserByPhoneNumber(string phoneNumber)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.PhoneNumber.ToLower().Equals(phoneNumber.ToLower()));
            return user; 
        }

        public async Task<User> GetUserByUsername(string username)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
            return user; 
        }
    }
}
