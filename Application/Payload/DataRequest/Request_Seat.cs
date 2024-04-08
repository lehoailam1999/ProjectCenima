using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
    public class Request_Seat
    {
        public int Number { get; set; }
        public string Line { get; set; }
        public int RoomId { get; set; }
        public int SeatStatusId { get; set; }
        public int SeatTypeId { get; set; }
    }
}
