using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Enums;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class Deal:BaseTenantEntity
    {
        public int? LeadId { get; set; }
        public Lead? Lead { get; set; } = null!;

        public int PipelineId { get; set; }
        public PipeLine PipeLine { get; set; } = null!;

        public int PipelineStageId { get; set; }
        public PipelineStage PipelineStage { get; set; } = null!;


        public int? CompanyId { get;  set; }
        public Company? Company { get;  set; }

        public int? ContactId { get;  set; }
        public Contact? Contact { get;  set; }

        public string Title { get; set; } = null!;
        //public string Stage { get; set; } = null!;
        public decimal Amount { get; set; }
        public int Probability { get; set; }

        // Not mapped / calculated
        public decimal ExpectedRevenue => Amount * Probability / 100m;
        public DateTime? ExpectedCloseDate { get; set; }
        public string? Description { get; set; } = null!;
        public int Order { get; set; }   // card order inside a stage

        public StageStatus Status { get; private set; }
        public ICollection<Address> Addresses { get; set; }

        public bool IsDeleted { get; set; } = false;    
        public DateTime DeletedAt { get; set; } = DateTime.MinValue;

        public int? AssignedUserId { get; set; }
        public int? CreatedByUserId { get; set; }   

        public User? AssignedUser { get; set; }
        public User? CreatedByUser { get; set; }

        public void ChangeStage(int newStageId, int newOrder)
        {
            //if (Status != StageStatus.Open)
            //    throw new Exception("Only open deals can be moved");

            PipelineStageId = newStageId;
            Order = newOrder;
        }

    }
}
