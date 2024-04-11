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
    public class Converter_Seat
    {
        private readonly IBaseRepositories<SeatStatus> _baseSeatStatusRepositories;
        private readonly IBaseRepositories<SeatType> _baseSeatTypeStatusRepositories;

        public Converter_Seat(IBaseRepositories<SeatStatus> baseSeatStatusRepositories, IBaseRepositories<SeatType> baseSeatTypeStatusRepositories)
        {
            _baseSeatStatusRepositories = baseSeatStatusRepositories;
            _baseSeatTypeStatusRepositories = baseSeatTypeStatusRepositories;
        }

        public Response_Seat EntityToDTO(Seat seat)
        {
            Response_Seat response = new Response_Seat()
            {
                Number = seat.Number,
                Line = seat.Line,
                RoomId = seat.RoomId,
                SeatStatusName = _baseSeatStatusRepositories.SingleOrDefault(x => x.Id == seat.SeatStatusId).NameStatus,
                SeatTypeName = _baseSeatTypeStatusRepositories.SingleOrDefault(x => x.Id == seat.SeatTypeId).NameType

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
                SeatStatusName = _baseSeatStatusRepositories.SingleOrDefault(x => x.Id == item.SeatStatusId).NameStatus,
                SeatTypeName = _baseSeatTypeStatusRepositories.SingleOrDefault(x => x.Id == item.SeatTypeId).NameType
            }).ToList();
        }
    }
}
