using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class PlanRepository:IplanRepository
    {
        private readonly AppDbContext _context;
        public PlanRepository(AppDbContext context) 
        {
            _context = context;
        }
        public async Task<bool> ExistsByNameAsync(string name)
        {
            return await  _context.Plans.AnyAsync(plan => plan.Name == name);
        }

        public async Task AddRangeAsync(IEnumerable<Plan> plans)
        {
            await _context.Plans.AddRangeAsync(plans);
            await _context.SaveChangesAsync();
        }

        public async Task<Plan> GetDefaultFreePlanAsync()
        {
            return await _context.Plans.FirstOrDefaultAsync(p => p.IsTrial);
        }

    }
}
