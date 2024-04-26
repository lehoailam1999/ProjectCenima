using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Org.BouncyCastle.Crypto.Engines.SM2Engine;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Payload.Converter
{
    public class Converter_Movie
    {
        public Response_Movie EntityToDTO(Movie movie)
        {
            Response_Movie response = new Response_Movie()
            {
                Id=movie.Id,
                MovieDuration = movie.MovieDuration,
                Description = movie.Description,
                EndTime=movie.EndTime,
                PremiereDate=movie.PremiereDate,
                Director=movie.Director,
                Image=movie.Image,
                HeroImage=movie.HeroImage,
                Language=movie.Language,
                Name=movie.Name,
                Trailer=movie.Trailer,
                RateId=movie.RateId,
                MovieTypeId=movie.MovieTypeId             
            };
            return response;
        }
        public List<Response_Movie> EntityToListDTO(List<Movie> listMovie)
        {

            return listMovie.Select(item => new Response_Movie
            {
                Id = item.Id,
                MovieDuration = item.MovieDuration,
                Description = item.Description,
                EndTime = item.EndTime,
                PremiereDate = item.PremiereDate,
                Director = item.Director,
                Image = item.Image,
                HeroImage = item.HeroImage,
                Language = item.Language,
                Name = item.Name,
                Trailer = item.Trailer,
                RateId = item.RateId,
                MovieTypeId = item.MovieTypeId
            }).ToList();
        }
    }
}
