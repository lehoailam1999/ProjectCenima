using Application.Payload.Converter.Converter_BillBook;
using Application.Payload.DataResponse;
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
        private readonly IBaseRepositories<Ticket> _baseTicketRepositories;
        private readonly Converter_Ticket _converter;

        public Converter_Schedules(IBaseRepositories<Room> baseRoomRepositories, IBaseRepositories<Movie> baseMovieRepositories, IBaseRepositories<Ticket> baseTicketRepositories, Converter_Ticket converter)
        {
            _baseRoomRepositories = baseRoomRepositories;
            _baseMovieRepositories = baseMovieRepositories;
            _baseTicketRepositories = baseTicketRepositories;
            _converter = converter;
        }

        public Response_Schedules EntityToDTO(Schedule schedule)
        {
            Response_Schedules response = new Response_Schedules()
            {
                Id=schedule.Id,
                StartAt = schedule.StartAt,
                EndAt = schedule.EndAt,
                Code = schedule.Code,
                IdRoom=schedule.RoomId,
                IdMovie= schedule.MovieId

            };
            if (schedule.Id != null)
            {
                var ticket = _baseTicketRepositories.Where(x => x.ScheduleId == schedule.Id);
                if (ticket != null)
                {
                    response.response_Tickets = ticket.Select(bt => _converter.EntityToDTO(bt)).ToList();
                }
            }
            return response;
        }
        public List<Response_Schedules> EntityToListDTO(List<Schedule> listSchedules)
        {
            List<Response_Schedules> list = new List<Response_Schedules>();
            foreach (var item in listSchedules)
            {
                Response_Schedules response = new Response_Schedules()
                {
                    Id=item.Id,
                    StartAt = item.StartAt,
                    EndAt = item.EndAt,
                    Code = item.Code,
                    IdRoom = item.RoomId,
                    IdMovie = item.MovieId

                };
                if (item.Id != null)
                {
                    var ticket = _baseTicketRepositories.Where(x => x.ScheduleId == item.Id);
                    if (ticket != null)
                    {
                        response.response_Tickets = ticket.Select(bt => _converter.EntityToDTO(bt)).ToList();
                    }
                }
                list.Add(response);
            }
            return list;
           /* return listSchedules.Select(item => new Response_Schedules
            {
                StartAt = item.StartAt,
                EndAt = item.EndAt,
                Code = item.Code,
                RoomName = _baseRoomRepositories.SingleOrDefault(x => x.Id == item.RoomId).Name,
                MovieName = _baseMovieRepositories.SingleOrDefault(x => x.Id == item.MovieId).Name

            }).ToList();*/
        }
    }
}
