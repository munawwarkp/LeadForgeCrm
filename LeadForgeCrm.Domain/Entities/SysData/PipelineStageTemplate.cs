using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Domain.Entities.SysData
{
    public class PipelineStageTemplate
    {
        public int Id { get; set; }
        public int TemplateId {  get; set; }
        public string Name {  get; set; } = null!;
        public int Order { get; set; }
        public int DefaultProbability { get; set; } 

        public PipelineTemplates Template { get; set; } = null!;
    }
}
