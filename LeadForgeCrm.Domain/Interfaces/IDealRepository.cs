using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Interfaces
{
    //commmand side
    public interface IDealRepository
    {
        Task<(List<Deal>, int totalCount)> GetDealsAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm,
            StageStatus? status,
            int? assignedUserId,
            CancellationToken ct
            );
        Task AddAsync(Deal deal);
        Task<Deal?> GetByIdAsync(int id, CancellationToken ct);
        Task UpdateAsync(Deal deal);
    }
}
