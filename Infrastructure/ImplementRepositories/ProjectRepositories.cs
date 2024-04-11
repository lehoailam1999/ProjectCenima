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

        public async Task<List<BillTicket>> GetAllBillTicket(int idBill)
        {
            var lstBillTicket = await _context.BillTickets
                .Include(bt => bt.ticket) // Ensure the ticket is loaded
                .Where(bt => bt.BillId == idBill)
                .ToListAsync();
            return lstBillTicket;
        }
        public async Task<List<BillFood>> GetAllBillFood(int idBill)
        {
            var lstBillFood = await _context.BillFoods
                .Include(bt => bt.food) // Ensure the ticket is loaded
                .Where(bt => bt.BillId == idBill)
                .ToListAsync();
            return lstBillFood;
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
