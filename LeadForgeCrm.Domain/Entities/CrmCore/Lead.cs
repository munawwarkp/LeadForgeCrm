using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Constants;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Lead :BaseTenantEntity
    {

        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string Phone { get; set; } = null!;

        public string LeadSource { get; set; } = null!;
        public string Status { get; set; } = LeadStatuses.New;

        public int? AssignedUserId { get; set; }
        public int? CreatedByUserId { get; set; }   

        public User? AssignedUser { get; set; }
        public User? CreatedByUser { get; set; }

        // This is the key for your flow
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }

        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        //public ICollection<Activity> Activities { get; set; } = new List<Activity>();
        public ICollection<Address> Addresses { get; set; } = new List<Address>();



        private static readonly Dictionary<LeadStatus, LeadStatus[]> AllowedTransitions = new()
        {
            [LeadStatus.New] = new[] { LeadStatus.Contacted },
            [LeadStatus.Contacted] = new[] { LeadStatus.Qualified, LeadStatus.Unqualified },
            [LeadStatus.Qualified] = new[] { LeadStatus.Converted }
        };


        public void UpdateStatus(LeadStatus newStatus)
        {
            var current = Enum.Parse<LeadStatus>(Status);

            if (!AllowedTransitions[current].Contains(newStatus))
            {
                throw new Exception("invalid status transition");
            }

            Status = newStatus.ToString();
        }

    }
}
