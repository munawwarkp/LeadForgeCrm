using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IplanRepository
    {
        Task<bool> ExistsByNameAsync(string name);
        Task AddRangeAsync(IEnumerable<Plan> plans);
        Task<Plan> GetDefaultFreePlanAsync();
    }
}
