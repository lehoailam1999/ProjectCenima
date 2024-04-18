using Application.Payload.DataResponse;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter.Converter_BillBook
{
    public class Convert_BillTickets
    {
        private readonly AppDbContext _context;

        public Convert_BillTickets()
        {
            _context = new AppDbContext();
        }

        public Response_BillTickets EntityToDTO(BillTicket billTicket)
        {
            var ticket1 = _context.Tickets.Include(x => x.schedule).ThenInclude(sc => sc.movie).SingleOrDefault(x => x.Id == billTicket.TicketId);
            var ticket2 = _context.Tickets.Include(x => x.schedule).ThenInclude(sc => sc.room).ThenInclude(r=>r.cinema).SingleOrDefault(x => x.Id == billTicket.TicketId);
            Response_BillTickets response = new Response_BillTickets()
            {
                Quantity=billTicket.Quantity,
                MovieName=ticket1.schedule.movie.Name,
                RoomName=ticket2.schedule.room.Name,
                CinemaName=ticket2.schedule.room.cinema.NameOfCinema,
                TicketId=billTicket.TicketId
                
            };
            return response;
        }
    }
}
