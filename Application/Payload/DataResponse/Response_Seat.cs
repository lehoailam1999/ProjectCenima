﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Seat
    {
        public int Number { get; set; }
        public string Line { get; set; }
        public bool IsActive { get; set; }
        public int RoomId { get; set; }
        public int SeatStatusId { get; set; }
        public int SeatTypeId { get; set; }
    }
}

