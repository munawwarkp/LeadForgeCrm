using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IPipelineStageRepository
    {
        Task UpdateStatus(PipelineStage pipelineStage);
        Task<PipelineStage?> GetFirstStageAsync(int pipelineId);
        Task<PipelineStage?> GetNextStageAsync(
            int pipelineId,
            int currentStageOrder);
    }
}
