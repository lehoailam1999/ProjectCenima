using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter.Converter_BillBook
{
    public class Convert_BillTickets
    {
        public Response_BillTickets EntityToDTO(BillTicket billTicket)
        {
            Response_BillTickets response = new Response_BillTickets()
            {
                Quantity=billTicket.Quantity,
                TicketId=billTicket.TicketId
                
            };
            return response;
        }
    }
}
