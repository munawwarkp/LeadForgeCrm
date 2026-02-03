using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Infrastructure.Data;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class PipelineRepository
    {
        private readonly AppDbContext _context;
        public PipelineRepository(AppDbContext context)
        {
            _context = context;
        }


    }
}
