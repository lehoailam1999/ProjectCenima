using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RankCustomer:BaseEntity
    {
        public int Point { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Promotion> promotion { get; set; }
        public IEnumerable<User> user { get; set; }


    }
}
