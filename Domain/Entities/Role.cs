using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Role:BaseEntity
    {
        public string Code { get; set; }
        public string RoleName { get; set; }
        public IEnumerable<User> user { get; set; }
    }
}
