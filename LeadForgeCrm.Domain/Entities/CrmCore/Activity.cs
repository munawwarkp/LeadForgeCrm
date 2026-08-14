using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Activity:BaseTenantEntity
    {
        public ActivityType Type { get; set; } // Call, Meeting, Email, etc.
        public string Description { get; set; } = null!;
        public DateTime ActivityDate { get; set; }

        public ActivityEntityType EntityType { get; set; } // Lead, Contact, Deal, etc.
        public int EntityId { get; set; } // Id of the associated entity (Lead, Contact, Deal, etc.)

        //public int LeadId { get; set; }
        //public Lead Lead { get; set; } = null!;  //many to one

         public int AssignedUserId { get; set; }
        public int? CreatedByUserId { get; set; }   

        public User AssignedUser { get; set; }
        public User? CreatedByUser { get; set; }

        public bool IsDeleted { get; set; } = false;

        public DateTime? DeletedAt { get; set; }

    }
}
