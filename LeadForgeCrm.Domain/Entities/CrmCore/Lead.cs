using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Constants;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Lead :BaseTenantEntity
    {

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string LeadSource { get; set; } = null!;
        public string Status { get; set; } = LeadStatuses.New;

        public int? AssignedToId { get; set; } //sales owner
        public User? AssignedTo { get; set; } = null!;

        // This is the key for your flow
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        public ICollection<Activity> Activities { get; set; } = new List<Activity>();

        [NotMapped]
        public bool IsConverted => Status == LeadStatuses.Converted;

    }
}
