using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Bill:BaseEntity
    {
        public double ToTalDouble { get; set; }
        public string TradingCode { get; set; }
        public DateTime CreateTime { get; set; }
        public string Name { get; set; }
        public DateTime UpdateTime { get; set; }
        [ForeignKey("BillStatusId")]
        public int BillStatusId { get; set; }
        public BillStatus? billStatus { get; set; }
        [ForeignKey("PromotionId")]
        public int PromotionId { get; set; }
        public Promotion? promotion { get; set; }

        [ForeignKey("UserId")]
        public int UserId { get; set; }
        public User? user { get; set; }

        public bool IsActive { get; set; }
        public IEnumerable<BillTicket>? billTicket { get; set; }
        public IEnumerable<BillFood>? billFood { get; set; }


    }
}
