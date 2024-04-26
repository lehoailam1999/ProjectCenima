using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Room
    {
        public int Id { get; set; } 
        public int Capacity { get; set; }
        public int Type { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public int CinemaId { get; set; }
    }
}
