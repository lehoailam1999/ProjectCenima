using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Schedule:BaseEntity
    {
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Code { get; set; }
        [ForeignKey("RoomId")]
        public int RoomId { get; set; }
        public Room room { get; set; }
        [ForeignKey("MovieId")]
        public int MovieId { get; set; }
        public Movie movie { get; set; }
        public bool IsActive { get; set; }
        public IEnumerable<Ticket> ticket { get; set; }


    }
}
