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
    public class ProjectRepositories : IProjectRepositories
    {
        private readonly AppDbContext _context;

        public ProjectRepositories(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<BillTicket>> GetAllBillTicket(int idBill)
        {
            var lstBillTicket = await _context.BillTickets
                .Include(bt => bt.ticket)
                .Where(bt => bt.BillId == idBill)
                .ToListAsync();
            return lstBillTicket;
        }
        public async Task<List<BillFood>> GetAllBillFood(int idBill)
        {
            var lstBillFood = await _context.BillFoods
                .Include(bt => bt.food) 
                .Where(bt => bt.BillId == idBill)
                .ToListAsync();
            return lstBillFood;
        }
    }
}
