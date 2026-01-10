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
            private readonly AppDbContext _context;
            public TenantRepository(AppDbContext context) 
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));
            }

            public Tenant Add(Tenant tenant)
            {
                var result = _context.Tenants.Add(tenant);
                //await _context.SaveChangesAsync();
                return result.Entity;
            }

            public async Task<Tenant?> GetIdByAsync(int tenantId)
            {
                return await _context.Tenants.FindAsync(tenantId);
            }
        }
}
