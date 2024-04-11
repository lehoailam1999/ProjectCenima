using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
    public class Request_Bill
    {
        public string Name { get; set; }
        public int PromotionId { get; set; }
        public int UserId { get; set; }
        public List<Request_BillTickets>? request_BillTickets { get; set; }
        public List<Request_BillFood>? request_BillFoods { get; set; }
    }
}
