using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RefreshToken:BaseEntity
    {
        public string Token { get; set; }
        public DateTime ExpiresTime { get; set; }
        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User user { get; set; }
    }
}
