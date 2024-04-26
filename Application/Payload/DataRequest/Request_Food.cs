using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
    public class Request_Food
    {
        public double Price { get; set; }
        public string Description { get; set; }
        public IFormFile Image { get; set; }
        public string NameofFood { get; set; }
    }
}
