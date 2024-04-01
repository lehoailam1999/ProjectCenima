using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Promotion:BaseEntity
    {
        public int Percent { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("RankCustomerId")]
        public int RankCustomerId { get; set; }
        public RankCustomer rankCustomer { get; set; }
        public IEnumerable<Bill> bill { get; set; }

    }
}
