using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Service.Services
{
    public class GetMovieServices : IGetMovieServices
    {
        private readonly IGetMovieRepositories _getMovieRepositories;

        public GetMovieServices(IGetMovieRepositories getMovieRepositories)
        {
            _getMovieRepositories = getMovieRepositories;
        }

        public async Task<IEnumerable<Movie>> GetMovieByIdCinema(int idCinema)
        {
            var list= await _getMovieRepositories.GetMovieByIdCinema(idCinema);
            return list;
        }
    }
}
