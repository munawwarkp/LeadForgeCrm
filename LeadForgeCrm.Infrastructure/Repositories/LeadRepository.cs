using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class LeadRepository:ILeadRepository
    {
        private readonly AppDbContext _context;
        public LeadRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddLead(Lead lead)
        {
            await _context.Leads.AddAsync(lead);
        }

        public async Task<bool> ExistsByEmailAsync(string email, int tenantId, CancellationToken ct)
        {
             return await _context.Leads.AnyAsync(l =>
                    l.Email == email &&
                    l.TenantId == tenantId,
                    ct
                    );
        }


        public async Task<Lead> GetByIdAsync(int leadId, CancellationToken ct)
        {
           return await _context.Leads.FirstOrDefaultAsync(l => l.Id == leadId, ct);
        }
        public async Task UpdateAsync(Lead lead)
        {
            _context.Leads.Update(lead);
        }

        public async Task DeleteAsync(int id)
        {
            await _context.Leads.
                Where(l => l.Id == id).
                ExecuteDeleteAsync();
        }

        public async Task<Lead?> GetLeadByIdAsync(int leadId, CancellationToken ct)
        {
            return await _context.Leads.
                AsNoTracking().
                FirstOrDefaultAsync(l => l.Id == leadId, ct);
               
        }

        public async Task<IEnumerable<Lead>> GetLeadsAsync(
            int pageNumber,
            int pageSize,
            CancellationToken ct)
        {
            return await _context.Leads.
                AsNoTracking().
                OrderByDescending(l => l.CreatedAt).
                Skip((pageNumber - 1)*pageSize).
                Skip(pageSize).
                ToListAsync();
        }
    }
}
