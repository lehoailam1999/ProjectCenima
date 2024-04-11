﻿using Application.Payload.DataResponse;
using Domain.Entities;
using Domain.InterfaceRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Schedules
    {
        private readonly IBaseRepositories<Room> _baseRoomRepositories;
        private readonly IBaseRepositories<Movie> _baseMovieRepositories;

        public Converter_Schedules(IBaseRepositories<Room> baseRoomRepositories, IBaseRepositories<Movie> baseMovieRepositories)
        {
            _baseRoomRepositories = baseRoomRepositories;
            _baseMovieRepositories = baseMovieRepositories;
        }

        public Response_Schedules EntityToDTO(Schedule schedule)
        {
            Response_Schedules response = new Response_Schedules()
            {
                StartAt = schedule.StartAt,
                EndAt = schedule.EndAt,
                Code = schedule.Code,
                RoomName = _baseRoomRepositories.SingleOrDefault(x=>x.Id==schedule.RoomId).Name,
                MovieName = _baseMovieRepositories.SingleOrDefault(x=>x.Id==schedule.MovieId).Name
            };
            return response;
        }
        public List<Response_Schedules> EntityToListDTO(List<Schedule> listSchedules)
        {
            return listSchedules.Select(item => new Response_Schedules
            {
                StartAt = item.StartAt,
                EndAt = item.EndAt,
                Code = item.Code,
                RoomName = _baseRoomRepositories.SingleOrDefault(x => x.Id == item.RoomId).Name,
                MovieName = _baseMovieRepositories.SingleOrDefault(x => x.Id == item.MovieId).Name

            }).ToList();
        }
    }
}