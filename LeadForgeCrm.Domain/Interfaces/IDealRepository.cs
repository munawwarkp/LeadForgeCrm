using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IDealRepository
    {
        Task AddAsync(Deal deal);
        Task<Deal?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(Deal deal);
    }
}
