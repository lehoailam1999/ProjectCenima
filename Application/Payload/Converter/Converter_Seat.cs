using Application.Payload.DataResponse;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Payload.Converter
{
    public class Converter_Seat
    {
        public Response_Seat EntityToDTO(Seat seat)
        {
            Response_Seat response = new Response_Seat()
            {
                Number = seat.Number,
                Line = seat.Line,
                RoomId = seat.RoomId,
                SeatStatusId = seat.SeatStatusId,
                SeatTypeId = seat.SeatTypeId

            };
            return response;
        }
        public List<Response_Seat> EntityToListDTO(List<Seat> listSeat)
        {
            return listSeat.Select(item => new Response_Seat
            {

                Number = item.Number,
                Line = item.Line,
                RoomId = item.RoomId,
                SeatStatusId = item.SeatStatusId,
                SeatTypeId = item.SeatTypeId
            }).ToList();
        }
    }
}
