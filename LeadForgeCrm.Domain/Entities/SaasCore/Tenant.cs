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
        public string? CompanyName { get; set; }
        public string? PhoneNumber { get; set; }
        public string Currency { get; set; } = "INR";

        public bool IsActive { get; set; } = true;
        public bool IsOnboardingCompleted { get; set; } = false;


        // One-to-one relationship with Subscription
        public Subscription? Subscription { get; set; } = null!; // nullable if a tenant may exist without subscription

        public ICollection<User> Users { get; set; }=new List<User>();
    }
}
