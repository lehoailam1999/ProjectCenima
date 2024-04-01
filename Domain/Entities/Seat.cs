using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Seat:BaseEntity
    {
        public int Number { get; set; }
        public string Line { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("RoomId")]
        public int RoomId { get; set; }
        public Room room { get; set; }
        [ForeignKey("SeatStatusId")]

        public int SeatStatusId { get; set; }
        public SeatStatus seatStatus { get; set; }

        [ForeignKey("SeatTypeId")]

        public int SeatTypeId { get; set; }
        public SeatType seatType { get; set; }
        public IEnumerable<Ticket> ticket { get; set; }


    }
}
