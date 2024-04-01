using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class GeneralSetting:BaseEntity
    {
        public DateTime BreakTime { get; set; }
        public int BusinessHours { get; set; }
        public DateTime CloseTime { get; set; }
        public double FixedTicketPrice { get; set; }
        public int PercentDay { get; set; }
        public int PercentWeekend { get; set; }
        public DateTime TimeBeginToChange { get; set; }
    }
}
