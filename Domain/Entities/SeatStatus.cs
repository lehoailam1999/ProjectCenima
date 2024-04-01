﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class SeatStatus:BaseEntity
    {
        public string Code { get; set; }
        public string NameStatus { get; set; }
        public IEnumerable<Room> room { get; set; }

    }
}
