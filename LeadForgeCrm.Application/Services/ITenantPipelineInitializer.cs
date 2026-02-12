using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;

namespace LeadForgeCrm.Application.Services
{
    public interface ITenantPipelineInitializer
    {
        Task CreateDefaultPipelineAsync(Tenant tenant, CancellationToken ct);
        //Task UpdateAsync();

    }
}
