using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Index("UserName", IsUnique = true)]

    public class User:BaseEntity
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Point { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        public string PhoneNumber { get; set; }
        [ForeignKey("RoleId")]
        public int RoleId { get; set; }
        public Role role { get; set; }
        [ForeignKey("RankCustomerId")]
        public int RankCustomerId { get; set; }
        public RankCustomer rankCustomer { get; set; }

        [ForeignKey("UserStatusId")]
        public int UserStatusId { get; set; }
        public UserStatus userStatus { get; set; }

        public IEnumerable<ConfirmEmail>? confirmEmail { get; set; }
        public IEnumerable<RefreshToken>? refreshToken { get; set; }
        public IEnumerable<Bill> bill { get; set; }


    }
}
