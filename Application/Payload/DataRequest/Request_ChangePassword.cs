using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
    public class Request_ChangePassword
    {
        [Required(ErrorMessage = "OldPassword is required")]
        public string OldPassword { get; set; }
        [Required(ErrorMessage = "NewPassword is required")]
        public string NewPassword { get; set; }
        [Required(ErrorMessage = "ConfirmPassword is required")]
        public string ConfirmPassword { get; set; }
    }
}
