using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Application.Interfaces
{
    public interface IActivityEntityValidator
    {
        Task<bool> ValidateAsync(
            ActivityEntityType entityType,
            int entityId,
            CancellationToken ct
            );
    }
}
