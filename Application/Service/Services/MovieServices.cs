using Application.Payload.Converter;
using Application.Payload.DataRequest;
using Application.Payload.DataResponse;
using Application.Payload.Response;
using Application.Service.IServices;
using Domain.Entities;
using Domain.InterfaceRepositories;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Application.Service.Services
{
    public class MovieServices : IMovieServices
    {
        private readonly IBaseRepositories<Movie> _baseMovieRepositories;
        private readonly Converter_Movie _converter;
        private readonly ResponseObject<Response_Movie> _respon;
        private readonly IPhotoServices _photoServices;

        public MovieServices(IBaseRepositories<Movie> baseMovieRepositories, Converter_Movie converter, ResponseObject<Response_Movie> respon, IPhotoServices photoServices)
        {
            _baseMovieRepositories = baseMovieRepositories;
            _converter = converter;
            _respon = respon;
            _photoServices = photoServices;
        }

        public async Task<ResponseObject<Response_Movie>> AddNewMovie(Request_Movie request)
        {
            Movie movie = new Movie();
            movie.MovieDuration = request.MovieDuration;
            movie.Description = request.Description;
            movie.EndTime = DateTime.Now;
            movie.PremiereDate = DateTime.Now;
            movie.Director = request.Director;
            movie.Image = await _photoServices.UploadPhoToAsync(request.Image);
            movie.HeroImage = request.HeroImage;
            movie.Language = request.Language;
            movie.Name = request.Name;
            movie.Trailer = request.Trailer;
            movie.RateId = request.RateId;
            movie.MovieTypeId = request.MovieTypeId;
            await _baseMovieRepositories.AddAsync(movie);
            return _respon.ResponseSuccess("Add movie successfully", _converter.EntityToDTO(movie));
        }
        public async Task<string> DeleteMovie(int id)
        {
            var cinemaDelete =await _baseMovieRepositories.FindAsync(id);
            if (cinemaDelete == null)
            {
                return "Not Found";
            }
            await _baseMovieRepositories.DeleteAsync(id);
            return "Delete Movie Successfully";
        }

        public async Task<ResponseObject<List<Response_Movie>>> GetAll()
        {
            ResponseObject<List<Response_Movie>> listRes = new ResponseObject<List<Response_Movie>>();
            var list = await _baseMovieRepositories.GetAll();
            return listRes.ResponseSuccess("Danh sach Movie", _converter.EntityToListDTO(list));
        }

        public async Task<ResponseObject<Response_Movie>> UpdateMovie(int id)
        {
            var movieUpdate = await _baseMovieRepositories.FindAsync(id);
            if (movieUpdate == null)
            {
                return _respon.ResponseError(StatusCodes.Status404NotFound, "Khong tim thay movie", null);
            }
            await _baseMovieRepositories.UpdateAsync(movieUpdate);
            return _respon.ResponseSuccess("Update movie Successfully", _converter.EntityToDTO(movieUpdate));
        }
    }
}
