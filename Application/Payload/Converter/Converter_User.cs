using Application.Payload.DataResponse;
using Application.Payload.Response;
using Domain.Entities;
using Domain.InterfaceRepositories;
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
        private readonly IBaseRepositories<Role> _baseRoleRepositories;
        private readonly IBaseRepositories<RankCustomer> _baseRankCustomerRepositories;
        private readonly IBaseRepositories<UserStatus> _baseUserStatussRepositories;

        public Converter_User(IBaseRepositories<Role> baseRoleRepositories, IBaseRepositories<RankCustomer> baseRankCustomerRepositories, IBaseRepositories<UserStatus> baseUserStatussRepositories)
        {
            _baseRoleRepositories = baseRoleRepositories;
            _baseRankCustomerRepositories = baseRankCustomerRepositories;
            _baseUserStatussRepositories = baseUserStatussRepositories;
        }

        public async Task<Response_Resgister> EntityToDTO(User user)
        {
            Response_Resgister response = new Response_Resgister()
            {
                UserName = user.UserName,
                Name = user.Name,
                Code =  (await _baseRoleRepositories.FindAsync(user.RoleId)).RoleName,
                Point = user.Point,
                Email = user.Email,
                PhoneNumber = user.PhoneNumber,
                Password = user.Password,
                RankCustomerName = (await _baseRankCustomerRepositories.FindAsync(user.RankCustomerId)).Name,
                UserStatusName = (await _baseUserStatussRepositories.FindAsync(user.UserStatusId)).Name,
            };
            return response;
        }
    }
}
