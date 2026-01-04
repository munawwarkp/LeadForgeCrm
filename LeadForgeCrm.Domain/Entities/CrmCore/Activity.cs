using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Activity:BaseTenantEntity
    {
        public string Type { get; set; } = null!; // Call, Meeting, Email, etc.
        public string Description { get; set; } = null!;
        public DateTime ActivityDate { get; set; }

        public int LeadId { get; set; }
        public Lead Lead { get; set; } = null!;

        public int UserId { get; set; }
        public User User { get; set; } = null!;

    }
}
