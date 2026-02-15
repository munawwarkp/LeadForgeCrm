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
    public class PipelineRepository: IPipelineRepository
    {
        private readonly AppDbContext _context;
        public PipelineRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(PipeLine pipeline)
        {
            await _context.PipeLines.AddAsync(pipeline);
            
        }

        public async Task<PipeLine> GetDefaultPipelineAsync()
        {
            return await _context.PipeLines.FirstOrDefaultAsync(p => p.IsDefault);
        }
    }
}
