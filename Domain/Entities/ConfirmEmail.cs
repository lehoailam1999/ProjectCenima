using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ConfirmEmail:BaseEntity
    {
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User user { get; set; }
        public DateTime ExpiredTime { get; set; }
        public string CodeActive { get; set; }
        public bool IsConfirm { get; set; } = false;
    }
}
