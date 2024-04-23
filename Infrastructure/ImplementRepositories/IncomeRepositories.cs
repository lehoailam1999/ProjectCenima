using Domain.Entities;
using Domain.InterfaceRepositories;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.ImplementRepositories
{
    public class IncomeRepositories : IIncomeRepositories
    {
        private readonly AppDbContext _context;

        public IncomeRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Food>> GetFoodHighLight(int itemAmount)
        {
            var sevenDaysAgo = DateTime.Now.AddDays(-7);

            var recentBills = _context.Bills
                .Where(b => b.CreateTime >= sevenDaysAgo&& b.BillStatusId==1)
                .Include(b => b.billFood)
                    .ThenInclude(bf => bf.food)
                .ToList();
            if (recentBills==null&&recentBills.Count==0)
            {
                return null;
            }
            var foodSales = recentBills.SelectMany(b => b.billFood)
                .GroupBy(bf => bf.food.Id)
                .Select(g => new 
                {
                    FoodId = g.Key, 
                    TotalQuantity = g.Sum(bf => bf.Quantity) 
                })
                .ToList();
            if (foodSales == null && foodSales.Count == 0)
            {
                return null;
            }
            var mostPopularFoods = foodSales.OrderByDescending(fs => fs.TotalQuantity)
                .Take(itemAmount) 
                .Select(fs => _context.Foods.Find(fs.FoodId))
                .ToList();
            if (mostPopularFoods == null && mostPopularFoods.Count == 0)
            {
                return null;
            }
            return mostPopularFoods;
        }

        public async Task<List<Bill>> GetIncome()
        {
            var cinemaSales = await _context.Bills
                .Include(b => b.billTicket)
            .ThenInclude(bt => bt.ticket)
                .ThenInclude(t => t.schedule)
                    .ThenInclude(sc => sc.movie) // Truy cập movie từ schedule
                        .ThenInclude(m => m.schedule) // Truy cập schedule từ movie (nếu cần)
                            .ThenInclude(s => s.room) // Truy cập room từ schedule
                                .ThenInclude(r => r.cinema) // Truy cập cinema từ room
                .ToListAsync();
            if (cinemaSales == null && cinemaSales.Count == 0)
            {
                return null;
            }

            return cinemaSales;
        }
    }
}
