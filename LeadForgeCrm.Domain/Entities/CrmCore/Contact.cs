using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Contact : BaseTenantEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Company { get; set; }

        public int OwnerUserId { get; set; }
        public User Owner { get; set; } = null!;


        public ICollection<Lead> Leads { get; set; } = new List<Lead>();
    }
}
