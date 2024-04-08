using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Room
    {
        public Response_Room EntityToDTO(Room room)
        {
            Response_Room response = new Response_Room()
            {
                Capacity = room.Capacity,
                Type = room.Type,
                Description = room.Description,
                Name = room.Name,
                Code = room.Code,
                CinemaId=room.CinemaId

            };
            return response;
        }
        public List<Response_Room> EntityToListDTO(List<Room> listroom)
        {
            return listroom.Select(item => new Response_Room
            {

                Capacity = item.Capacity,
                Type = item.Type,
                Description = item.Description,
                Name = item.Name,
                Code = item.Code,
                CinemaId = item.CinemaId

            }).ToList();
        }
    }
}
