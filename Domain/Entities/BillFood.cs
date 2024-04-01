using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class BillFood:BaseEntity
    {
        public int Quantity { get; set; }
        [ForeignKey("BillId")]
        public int BillId { get; set; }
        public Bill bill { get; set; }
        [ForeignKey("FoodId")]

        public int FoodId { get; set; }
        public Food food { get; set; }


    }
}
