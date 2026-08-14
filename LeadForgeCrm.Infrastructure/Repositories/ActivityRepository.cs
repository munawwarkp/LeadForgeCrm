using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class ActivityRepository(AppDbContext context): IActivityRepository
    {
        public async Task AddAsync(Activity activity, CancellationToken ct)
        {
             context.Activities.Add(activity);

            //await context.SaveChangesAsync(ct);
        }

        public async Task<List<Activity>> GetActivitiesByEntityAsync(
            int entityId,
            ActivityEntityType entityType,
            CancellationToken ct
            )
        {
            return await context.Activities
                    .AsNoTracking()
                    .Where(a => 
                         a.EntityId == entityId &&
                         a.EntityType == entityType)
                    .ToListAsync(ct);

        }

        public async Task<Activity?> GetActivityByIdAsync(int id, CancellationToken ct)
        {
            return await context.Activities
                  .FirstOrDefaultAsync(a => a.Id == id); 
        }

    }
}
