using Application.Payload.DataRequest;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Schedules 
    {
        public int Id { get; set; }
        public DateTime StartAt { get; set; }
        public DateTime EndAt { get; set; }
        public string Code { get; set; }
        public int IdRoom { get; set; }
        public int IdMovie { get; set; }
        public bool IsActive { get; set; }
        public List<Response_Ticket>? response_Tickets { get; set; }

    }
}
