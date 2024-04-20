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
        Task<List<BillTicket>> GetAllBillTicket(int idBill);
        Task<List<BillFood>> GetAllBillFood(int idBill);


    }
}
