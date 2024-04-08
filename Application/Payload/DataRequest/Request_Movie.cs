using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
    public class Request_Movie
    {
        public int MovieDuration { get; set; }
        /*public DateTime EndTime { get; set; }
        public DateTime PremiereDate { get; set; }*/
        public string Description { get; set; }
        public string Director { get; set; }
        public string Image { get; set; }
        public string HeroImage { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Trailer { get; set; }
        public int RateId { get; set; }
        public int MovieTypeId { get; set; }
    }
}
