using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Entities.SaasCore;

namespace LeadForgeCrm.Domain.Entities
{
    public class User : BaseTenantEntity
    {
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string PasswordHash { get; set; } = null!;

        public int RoleId { get; set; }
        public Role Role { get; set; } 

        public bool IsActive { get; set; } = true;

        public Tenant Tenant { get; set; } = null!;


        // Optional: list of activities performed by this user
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();


    }
}
