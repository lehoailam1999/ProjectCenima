using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Token
    {
        public string AccessToken { get; set; }
        public string RefreshToken { get; set; }
    }
}
