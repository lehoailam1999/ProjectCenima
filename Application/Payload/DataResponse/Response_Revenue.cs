﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Revenue
    {

        public int Id { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public string NameOfCinema { get; set; }
        public double ToTalRevenue { get; set; }
    }
   
}
