using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface ILeadRepository
    {
        Task AddLead(Lead lead);
        Task<bool> ExistsByEmailAsync(string email, int tenantId, CancellationToken ct);

        Task<Lead> GetByIdAsync(int leadId, CancellationToken ct);
        Task UpdateAsync(Lead lead);
        Task DeleteAsync(int id);


        Task<Lead?> GetLeadById(int leadId);
        Task<IEnumerable<Lead>> GetLeads();
    }
}
