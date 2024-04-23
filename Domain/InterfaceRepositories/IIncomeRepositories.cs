using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IIncomeRepositories
    {
        Task<List<Bill>> GetIncome();
        Task<List<Food>> GetFoodHighLight(int itemAmount);
    }
}
