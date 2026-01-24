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
    }
}
