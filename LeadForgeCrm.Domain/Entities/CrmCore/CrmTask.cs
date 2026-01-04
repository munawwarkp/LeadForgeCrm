using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class CrmTask:BaseTenantEntity
    {
        //followup
        public string Title { get; set; } = null!;
        public DateTime DueDate { get; set; }
        public string Status { get; set; } = null!;

        public int AssignedToUserId { get; set; }
        public int? LeadId { get; set; }
    }
}
