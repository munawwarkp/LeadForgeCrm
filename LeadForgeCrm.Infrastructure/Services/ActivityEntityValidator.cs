using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Services
{
    public class ActivityEntityValidator(
        AppDbContext dbContext
        ) : IActivityEntityValidator
    {
        public async Task<bool> ValidateAsync(
            ActivityEntityType entityType,
            int entityId, 
            CancellationToken ct)
        {
            var exist = entityType switch
            {
                ActivityEntityType.Deal =>
                    await dbContext.Deals.AnyAsync(d => d.Id == entityId, ct),

                ActivityEntityType.Lead =>
                    await dbContext.Leads.AnyAsync(l => l.Id == entityId, ct),

                ActivityEntityType.Contact =>
                    await dbContext.Contacts.AnyAsync(c => c.Id == entityId, ct),

                _ => false
            };

            return exist;
        }
    }
}
