using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class TenantRepository:ITenantRepository
    {
        private readonly TenantlessDbContext _context;
        public TenantRepository(TenantlessDbContext context) 
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public async Task<Tenant> AddAsync(Tenant tenant)
        {
            var result = _context.Tenants.Add(tenant);
            await _context.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<Tenant?> GetIdByAsync(int tenantId)
        {
            return await _context.Tenants.FindAsync(tenantId);
        }
    }
}
