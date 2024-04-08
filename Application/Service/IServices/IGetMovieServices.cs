using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.IServices
{
    public interface IGetMovieServices
    {
        Task<IEnumerable<Movie>> GetMovieByIdCinema(int idCinema);

    }
}
