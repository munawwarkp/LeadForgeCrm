using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Deal:BaseTenantEntity
    {
        public int LeadId { get; set; }
        public Lead Lead { get; set; } = null!;

        public int PipelineStageId { get; set; }
        public PipelineStage PipelineStage { get; set; } = null!;

        public string Title { get; set; } = null!;
        //public string Stage { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }

    }
}
