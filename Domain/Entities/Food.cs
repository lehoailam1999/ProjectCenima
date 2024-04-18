using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Food:BaseEntity
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public string NameofFood { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<BillFood>? billFood { get; set; }

    }
}
