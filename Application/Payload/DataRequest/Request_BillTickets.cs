using Domain.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.DataRequest
{
    public class Request_BillTickets
    {
        public int TicketId { get; set; }
    }
}
