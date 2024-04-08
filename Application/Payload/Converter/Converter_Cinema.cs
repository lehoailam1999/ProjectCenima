using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Cinema
    {
        public Response_Cinema EntityToDTO(Cinema cinema)
        {
            Response_Cinema response = new Response_Cinema()
            {
                Address = cinema.Address,
                Code = cinema.Code,
                NameOfCinema = cinema.NameOfCinema,
                Description = cinema.Description
            };
            return response;
        }
        public List<Response_Cinema> EntityToListDTO(List<Cinema> listcinema)
        {
            return listcinema.Select(item => new Response_Cinema
            {
                Address = item.Address,
                Code = item.Code,
                NameOfCinema = item.NameOfCinema,
                Description = item.Description
            }).ToList();
        }
    }
}
