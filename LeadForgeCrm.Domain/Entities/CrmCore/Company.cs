using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Company:BaseTenantEntity
    {
        public string Name { get; set; } = null!;

        public ICollection<Contact> Contacts { get; set; } = new List<Contact>();
    }
}
