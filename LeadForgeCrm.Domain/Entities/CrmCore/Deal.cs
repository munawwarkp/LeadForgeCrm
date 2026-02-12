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
        public int LeadId { get; set; }
        public Lead Lead { get; set; } = null!;

        public int PipelineId { get; set; }

        public int PipelineStageId { get; set; }
        public PipelineStage PipelineStage { get; set; } = null!;


        public int? CompanyId { get; private set; }
        public Company? Company { get; private set; }

        public int? ContactId { get; private set; }
        public Contact? Contact { get; private set; }

        public string Title { get; set; } = null!;
        //public string Stage { get; set; } = null!;
        public decimal Amount { get; set; }
        public DateTime? ExpectedCloseDate { get; set; }

        public int Order { get; set; }   // card order inside a stage

        public StageStatus Status { get; private set; }


        public void ChangeStage(int newStageId, int newOrder)
        {
            //if (Status != StageStatus.Open)
            //    throw new Exception("Only open deals can be moved");

            PipelineStageId = newStageId;
            Order = newOrder;
        }

        public void AssignCompany(int companyId)
        {
            CompanyId = companyId;
        }

    }
}
