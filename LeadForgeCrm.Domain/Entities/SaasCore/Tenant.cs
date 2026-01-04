using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.SaasCore
{
    public class Tenant : BaseEntity    
    {
        public string CompanyName { get; set; } = null!;
        public string Subdomain { get; set; } = null!;
        public int PlanId { get; set; }
        public bool IsActive { get; set; } = true;

        public Plan Plan { get; set; } = null!;

        public ICollection<User> Users { get; set; }=new List<User>();
    }
}
