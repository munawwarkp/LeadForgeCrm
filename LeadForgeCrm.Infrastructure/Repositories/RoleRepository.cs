using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class RoleRepository:IRoleRepository
    {
        private readonly AppDbContext _context;
        public RoleRepository(AppDbContext context)
        { 
            _context = context;
        }

        public void Add(Role role)
        {
             _context.Roles.AddAsync(role);
            //await _context.SaveChangesAsync();
        }

        public async Task<Role?> GetByNameAsync(int tenantId, string roleName)
        {
            return await _context.Roles
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(r =>
                    r.Name == roleName
                );
        }
    }
}
