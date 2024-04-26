using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Enumerates
{
    public class CinemaRevenue
    {
        public int Id { get; set; }
        public string NameOfCinema { get; set; }
        public string Address { get; set; }
        public string Description { get; set; }
        public double TotalRevenue { get; set; }
    }
}
