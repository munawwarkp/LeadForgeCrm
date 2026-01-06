using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Constants;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Lead :BaseTenantEntity
    {
        public int ContactId { get; set; }  
        public Contact Contact { get; set; } = null!;

        public string LeadSource { get; set; } = null!;
        public string Status { get; set; } = LeadStatuses.New;

        public int AssignedToId { get; set; } //lead owner
        public User AssignedTo { get; set; } = null!;


        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();


    }
}
