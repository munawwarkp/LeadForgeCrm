using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class PipelineStage : BaseTenantEntity
    {
        public int PipelineId { get; set; }
        public PipeLine Pipeline { get; set; } = null!;

        public string Name { get; set; } = null!;
        // Example: New Lead, Site Visit, Negotiation, Closed Won
        public int Order { get; set; }
        // Used for drag & drop,column ordering

        //public bool IsClosed { get; set; }
        //public bool IsWon { get; set; }
        
        public int DeafultProbability { get; set; } 
        public StageStatus Status { get; private set; }


        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        //for get all deals in a stage

        public void MarkAsWon()
        {
            if (Status != StageStatus.Open)
                throw new Exception("Onlly open stages can be won");

            Status = StageStatus.Won;
        }

        public void ChangeOrder(int order)
        {
            if (Status != StageStatus.Open)
                throw new Exception("Closed stages cannot be reordered");

            Order = order;
        }
    }
}
