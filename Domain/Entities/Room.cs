using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Room:BaseEntity
    {
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
      
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("CinemaId")]
        public int CinemaId { get; set; }
        public Cinema cinema { get; set; }
        public IEnumerable<Schedule> schedule { get; set; }
/*        public IEnumerable<Room> room { get; set; }
*/    }
}
