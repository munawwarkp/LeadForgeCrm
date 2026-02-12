using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface ICompanyRepository
    {
        Task AddAsync(Company company, CancellationToken ct);
        Task<bool> ExistsAsync(int companyId, CancellationToken ct);
    }
}
