using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IRoleRepository
    {
        void Add(Role role);
        Task<Role?> GetByNameAsync(int tenantId, string roleName);
    }
}
