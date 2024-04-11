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
    public class GetMovieRepositories : IGetMovieRepositories
    {
        private readonly AppDbContext _context;

        public GetMovieRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Movie>> GetMovieByIdCinema(int idCinema)
        {
            var listMovie = await _context.Movies
                 .Include(m => m.schedule)
                 .ThenInclude(s => s.room)
                 .Where(m => m.schedule.Any(s => s.room.CinemaId == idCinema))
                 .ToListAsync();
            return listMovie;

        }

        public async Task<List<Movie>> GetMovieByIdRoom(int idRoom)
        {
            var listMOvie = await _context.Movies.Include(m => m.schedule).Where(m => m.schedule.Any(x => x.RoomId == idRoom)).ToListAsync();
            return listMOvie;
        }

        public async Task<List<Movie>> GetMovieGighLight()
        {
            var billsWithStatusOne = await _context.Bills
                    .Where(b => b.BillStatusId == 1)
                    .Include(b => b.billTicket)
                    .ThenInclude(bt => bt.ticket)
                    .ToListAsync();
            if (billsWithStatusOne==null&& billsWithStatusOne.Count==0)
            {
                return null;
            }
            var scheduleIds = billsWithStatusOne
                .SelectMany(b => b.billTicket)
                .Select(bt => bt.ticket.ScheduleId)
                .Distinct()
                .ToList();
            if (scheduleIds == null && scheduleIds.Count == 0)
            {
                return null;
            }
            var moviesWithQuantity = await _context.Movies
                .Where(m => m.schedule
                .Any(s => scheduleIds.Contains(s.Id)))
                .ToListAsync();
            if (moviesWithQuantity == null && moviesWithQuantity.Count == 0)
            {
                return null;
            }
            return moviesWithQuantity;

        }
    }
}
