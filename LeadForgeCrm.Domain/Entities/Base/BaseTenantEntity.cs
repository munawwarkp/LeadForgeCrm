using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Domain.Entities.Base
{
    public abstract class BaseTenantEntity :BaseEntity
    {
        public int TenantId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
