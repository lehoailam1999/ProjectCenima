using Domain.Entities;
using Domain.Enumerates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.InterfaceRepositories
{
    public interface IIncomeRepositories
    {
        Task<List<CinemaRevenue>> GetIncome(DateTime startAt, DateTime endAt);
        Task<List<Food>> GetFoodHighLight(int itemAmount);
    }
}
