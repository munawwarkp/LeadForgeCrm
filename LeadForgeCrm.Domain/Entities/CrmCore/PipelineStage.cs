using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class PipelineStage:BaseTenantEntity
    {
        public int PipelineId { get; set; }
        public PipeLine Pipeline { get; set; } = null!;

        public string Name { get; set; } = null!;
        // Example: New Lead, Site Visit, Negotiation, Closed Won
        public int Order { get; set; }
        // Used for drag & drop,column ordering

        public bool IsClosed { get; set; }
        public bool IsWon { get; set; }



        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        //for get all deals in a stage

    }
}
