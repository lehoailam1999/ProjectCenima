using Domain.Entities;
using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplementRepositories
{
    public class ProjectRepositories : IProjectRepositories
    {
        private readonly AppDbContext _context;

        public ProjectRepositories(AppDbContext context)
        {
            _context = context;
        }
        public async Task<Cinema> GetCinema(int id)
        {
            var cinema = await _context.Cenimas.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return cinema;
        }

        public async Task<Room> GetRoom(int id)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return room;
        }

        public async Task<SeatStatus> GetSeatStatus(int id)
        {
            var seatstatus = await _context.SeatStatuses.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return seatstatus;
        }

        public async Task<SeatType> GetSeatType(int id)
        {
            var seatTypes = await _context.SeatTypes.SingleOrDefaultAsync(x => x.Id.Equals(id));
            return seatTypes;
        }
    }
}
