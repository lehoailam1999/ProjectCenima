using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataResponse
{
    public class Response_Bill
    {
        public double ToTalDouble { get; set; }
        public string Name { get; set; }
        public int BillStatusId { get; set; }
        public int PromotionId { get; set; }
        public int UserId { get; set; }
        public List<Response_BillTickets>? response_BillTickets { get; set; }
        public List<Response_BillFood>? response_BillFoods { get; set; }
    }
}
