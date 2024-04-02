using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Resgister
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public int Point { get; set; }
        public string Email { get; set; }
        [Required(ErrorMessage = "Password is required.")]
        
        public string Password { get; set; }
/*        public bool IsActive { get; set; }
*/        public string PhoneNumber { get; set; }
        public string Code { get; set; }
        public string RankCustomerName { get; set; }
        public string UserStatusName { get; set; }
    }
}
