using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Domain.Entities.SysData
{
    public class PipelineTemplates
    {
        public int Id { get; set; }
        public string Industry { get; set; } = null!;
        public string Name { get; set; } = null!;


        public ICollection<PipelineStageTemplate> Stages { get; set; }
    = new List<PipelineStageTemplate>();

    }
}
