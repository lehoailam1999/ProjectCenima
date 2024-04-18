using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
        public class Request_Schedules
        {
            public DateTime StartAt { get; set; }
            public DateTime EndAt { get; set; }
            public int RoomId { get; set; }
            public int MovieId { get; set; }
            public List<Request_Ticket>? request_Tickets { get; set; }

         }
}
