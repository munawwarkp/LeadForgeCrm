using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SysData;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class PipelineTemplateRepo: IPipelineTemplateRepository
    {
        private readonly AppDbContext _context;
        public PipelineTemplateRepo(AppDbContext context)
        {
            _context = context; 
        }

        public async Task<PipelineTemplates> GetDefaultAsync()
        {
            return await _context.PipelineTemplates
                .Include(p => p.Stages)          
                .FirstOrDefaultAsync(p => p.Id == 1);           
        }
    }
}
