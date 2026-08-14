using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IActivityRepository
    {
        Task AddAsync(Activity activity, CancellationToken ct);

        Task<List<Activity>> GetActivitiesByEntityAsync(
            int entityId,
            ActivityEntityType entityType,
            CancellationToken ct
            );

        Task<Activity?> GetActivityByIdAsync(int id, CancellationToken ct);

    }
}
