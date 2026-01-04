using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Entities.SaasCore
{
    public class Plan:BaseEntity
    {
        public string Name { get; set; } = null!;
        public decimal Price { get; set; }

        public BillingInterval BillingInterval { get; set; }
        public string Currency { get; set; } = "INR";

        public int MaxUsers { get; set; }
        public int MaxLeads { get; set; }

        public bool IsActive { get; set; } = true;

        public ICollection<Tenant> Tenants { get; set; } = new List<Tenant>();

    }
}
