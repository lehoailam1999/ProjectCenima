﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_BillTickets
    {
        public int Quantity { get; set; }
        public int TicketId { get; set; }
        public string CinemaName { get; set; }
        public string RoomName { get; set; }
        public string MovieName { get; set; }

    }
}
