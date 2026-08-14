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
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string? Phone { get; set; }


        //public string Company { get; set; }

        public int? CompanyId { get; set; }
        public Company? Company { get; set; } = null!;

        public int OwnerId { get; set; }
        public User Owner { get; set; } = null!;


        public ICollection<Lead> Leads { get; set; } = new List<Lead>();
        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();
    }
}
