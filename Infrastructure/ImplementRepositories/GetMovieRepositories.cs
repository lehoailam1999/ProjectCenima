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
    public class GetMovieRepositories:IGetMovieRepositories
    {
        private readonly AppDbContext _context;

        public GetMovieRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Movie>> GetMovieByIdCinema(int idCinema)
        {
           var listMovie = await _context.Movies
                .Include(m=>m.schedule)
                .ThenInclude(s => s.room)
                .ThenInclude(r=>r.cinema)
                .Where(m=>m.schedule.Any(s=>s.room.cinema.Id==idCinema))
                .ToListAsync();
            return listMovie;

        }
    }
}
