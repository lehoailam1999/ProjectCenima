using Application.Payload.DataResponse;
using Application.Payload.Response;
using Domain.Entities;
using Infrastructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_User
    {
        private readonly AppDbContext _appDbContext;

        public Converter_User(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Response_Resgister EntityToDTO(User user)
        {
            Response_Resgister response = new Response_Resgister()
            {
                UserName = user.UserName,
                Name = user.Name,
                Code = _appDbContext.Roles.SingleOrDefault(x => x.Id == user.RoleId).RoleName,
                Point = user.Point,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                RankCustomerName = _appDbContext.RankCustomers.SingleOrDefault(x => x.Id == user.RankCustomerId).Name,
                UserStatusName = _appDbContext.UserStatuss.SingleOrDefault(x => x.Id == user.UserStatusId).Name,
            };
            return response;
        }
    }
}
