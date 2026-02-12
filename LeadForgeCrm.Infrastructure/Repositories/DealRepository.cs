using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly AppDbContext _context;
        public DealRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(Deal deal)
        {
            await _context.Deals.AddAsync(deal);
        }

        public async Task<Deal?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Deals.FindAsync(id);
        }

        public async Task UpdateAsync(Deal deal)
        {
            _context.Deals.Update(deal);
        }
    }
}
