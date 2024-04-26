using Domain.Entities;
using Domain.Enumerates;
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

            var recentBills = await _context.Bills
                .Where(b => b.CreateTime >= sevenDaysAgo&& b.BillStatusId==1)
                .Include(b => b.billFood)
                .ThenInclude(bf => bf.food)
                .ToListAsync();
            if (recentBills==null&&recentBills.Count==0)
            {
                return null;
            }
            var foodSales = recentBills
                .SelectMany(b => b.billFood)
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
            var mostPopularFoods = foodSales
                .OrderByDescending(fs => fs.TotalQuantity)
                .Take(itemAmount) 
                .Select(fs => _context.Foods.Find(fs.FoodId))
                .ToList();
            if (mostPopularFoods == null && mostPopularFoods.Count == 0)
            {
                return null;
            }
            return mostPopularFoods;
        }

        public async Task<List<CinemaRevenue>> GetIncome(DateTime startAt, DateTime endAt)
        {
            /*var cinemaSales = await _context.Bills
               .Include(b => b.billTicket)
                   .ThenInclude(bt => bt.ticket)
                       .ThenInclude(t => t.schedule)
                             .ThenInclude(s => s.room)
                                  .ThenInclude(r => r.cinema)
               .ToListAsync();*/
            
            var listCinema = await _context.Bills
                    .AsNoTracking()
                    .Where(b => b.CreateTime >= startAt && b.CreateTime <= endAt && b.BillStatusId == 1)
                    .GroupBy(b => b.billTicket.FirstOrDefault().ticket.schedule.room.cinema.Id)
                    .Select(g => new CinemaRevenue
                    {
                        Id = g.FirstOrDefault().billTicket.FirstOrDefault().ticket.schedule.room.cinema.Id,
                        NameOfCinema = g.FirstOrDefault().billTicket.FirstOrDefault().ticket.schedule.room.cinema.NameOfCinema,
                        Address = g.FirstOrDefault().billTicket.FirstOrDefault().ticket.schedule.room.cinema.Address,
                        Description = g.FirstOrDefault().billTicket.FirstOrDefault().ticket.schedule.room.cinema.Description,
                        TotalRevenue = g.Sum(b => b.ToTalDouble)
                    })
                    .ToListAsync();
            if (listCinema == null && listCinema.Count == 0)
            {
                return null;
            }
            return listCinema;
        }
    }
}
