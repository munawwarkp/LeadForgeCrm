using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.CrmCore
{
    public class PipeLine:BaseTenantEntity
    {
        public string Name { get; set; } = null!;
        public bool IsDefault { get; set; }

        public ICollection<PipelineStage> Stages { get; set; } = new List<PipelineStage>();
        public ICollection<Deal> Deals { get; set; } = new List<Deal>();
        //multiple pipelines per tenant
    }

}
