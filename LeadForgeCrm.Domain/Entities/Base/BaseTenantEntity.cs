using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;

namespace LeadForgeCrm.Domain.Entities.Base
{
    public abstract class BaseTenantEntity :BaseEntity
    {
        public int TenantId { get; set; }
        public Tenant Tenant { get; set; }
    }
}
