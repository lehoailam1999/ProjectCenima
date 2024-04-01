using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class MovieTpye:BaseEntity
    {
        public string MovieTypeName { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Movie> movie { get; set; }

    }
}
