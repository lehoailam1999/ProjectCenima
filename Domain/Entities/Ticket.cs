using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Ticket:BaseEntity
    {
        public string Code { get; set; }
       
        public double PriceTicket { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("ScheduleId")]
        public int ScheduleId { get; set; }
        public Schedule schedule { get; set; }
        [ForeignKey("SeatId")]
        public int SeatId { get; set; }
        public Seat seat { get; set; }
        public IEnumerable<BillTicket> billTickets { get; set; }


    }
}
