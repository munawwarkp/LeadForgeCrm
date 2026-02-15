using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class PipelineStageRepository : IPipelineStageRepository
    {
        private readonly AppDbContext _context;
        public PipelineStageRepository(AppDbContext context)
        {
            _context = context;
        }



        public async Task UpdateStatus(PipelineStage pipelineStage)
        {
            _context.PipelineStages.Update(pipelineStage);
        }

        public async Task<PipelineStage?> GetFirstStageAsync(int pipelineId)
        {
           return await _context.PipelineStages
                .Where(s => s.PipelineId == pipelineId && s.Status == Domain.Enums.StageStatus.Open)
                .OrderBy(s => s.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<PipelineStage?> GetNextStageAsync(
            int pipelineId,
            int currentStageOrder)
        {
            return await _context.PipelineStages
                .Where(s =>
                    s.PipelineId == pipelineId && 
                    s.Order == currentStageOrder)
                .OrderBy(s => s.Order)
                .FirstOrDefaultAsync();
        }

        public async Task<PipelineStage?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.PipelineStages.FindAsync(id, ct);
        }
    }
}
