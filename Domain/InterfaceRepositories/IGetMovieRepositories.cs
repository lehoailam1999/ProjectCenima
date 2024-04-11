using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IGetMovieRepositories
    {
        Task<List<Movie>> GetMovieByIdCinema(int idCinema);
        Task<List<Movie>> GetMovieByIdRoom(int idRoom);
        Task<List<Movie>> GetMovieGighLight();
    }
}
