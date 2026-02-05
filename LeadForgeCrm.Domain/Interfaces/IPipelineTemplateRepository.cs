using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SysData;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IPipelineTemplateRepository
    {
        Task<PipelineTemplates> GetDefaultAsync();
    }
}
