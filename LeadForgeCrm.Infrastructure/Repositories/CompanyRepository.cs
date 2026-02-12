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
    public class CompanyRepository : ICompanyRepository
    {
        private readonly AppDbContext _context;
        public CompanyRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Company company, CancellationToken ct)
        {
            await _context.Companies.AddAsync(company, ct);
        }

        public async Task<bool> ExistsAsync(int companyId, CancellationToken ct)
        {
            return await _context.Companies.AnyAsync(c => c.Id ==companyId, ct);
        }
    }
}
