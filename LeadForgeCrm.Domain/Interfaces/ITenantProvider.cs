using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface ITenantProvider
    {
        int TenantId { get; }   
    }
}
