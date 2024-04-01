using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Rate:BaseEntity
    {
        public string Description { get; set; }
        public string Code { get; set; }
        public IEnumerable<Movie> movie { get; set; }
    }
}
