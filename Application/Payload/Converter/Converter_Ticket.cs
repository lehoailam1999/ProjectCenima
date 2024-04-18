using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Ticket
    {
        public Response_Ticket EntityToDTO(Ticket ticket)
        {
            Response_Ticket response = new Response_Ticket()
            {
                PriceTicket = ticket.PriceTicket,
                SeatId = ticket.SeatId

            };
            return response;
        }
    }
}
