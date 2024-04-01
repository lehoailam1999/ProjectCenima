using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Movie:BaseEntity
    {
        public int MovieDuration { get; set; }
        public DateTime EndTime { get; set; }
        public DateTime PremiereDate { get; set; }
        public string Description { get; set; }
        public string Director { get; set; }
        public string Image { get; set; }
        public string HeroImage { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Trailer { get; set; }
        [ForeignKey("RateId")]
        public int RateId { get; set; }
        public Rate rate { get; set; }
        [ForeignKey("MovieTypeId")]
        public int MovieTypeId { get; set; }
        public MovieTpye movieTpye { get; set; }
        public IEnumerable<Schedule> schedule { get; set; }



    }
}
