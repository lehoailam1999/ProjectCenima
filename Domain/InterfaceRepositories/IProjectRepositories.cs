using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IProjectRepositories
    {
        Task<Cinema> GetCinema(int id);
        Task<Room> GetRoom(int id);
        Task<SeatStatus> GetSeatStatus(int id);
        Task<SeatType> GetSeatType(int id);
        Task<List<BillTicket>> GetAllBillTicket(int idBill);
        Task<List<BillFood>> GetAllBillFood(int idBill);


    }
}
