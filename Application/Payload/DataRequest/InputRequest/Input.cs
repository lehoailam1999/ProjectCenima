using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest.InputRequest
{
    public class Input
    {
        public DateTime? StartAt { get; set; }
        public DateTime? EndAt { get; set; }
        public int? TotalMovieHighLight { get; set; }
/*        public string HeroName { get; set; }
*/
    }
}
