using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class DealRepository : IDealRepository
    {
        private readonly AppDbContext _context;
        public DealRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task<(List<Deal>, int totalCount)> GetDealsAsync(
            int pageNumber,
            int pageSize,
            string? searchTerm,
            StageStatus? status,
            int? assignedUserId,
            CancellationToken ct
            )
        {

            var query = _context.Deals
                .AsNoTracking()
                .AsQueryable();

            if (status.HasValue)
            {
                query = query.Where(d => d.Status == status.Value);
            }

            if (assignedUserId.HasValue)
            {
                query = query.Where(d => d.AssignedUserId == assignedUserId.Value);
            }

            //searrching
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(d =>
                    d.Title.Contains(searchTerm)
                );
            }
            
            var totalCount = await query.CountAsync(ct);

            //pagination
            var deals = await query
                .OrderByDescending(d => d.Id)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync(ct);

            return (deals, totalCount);
        }
        public async Task AddAsync(Deal deal)
        {
            await _context.Deals.AddAsync(deal);
        }

        public async Task<Deal?> GetByIdAsync(int id, CancellationToken ct)
        {
            return await _context.Deals.FindAsync(id);
        }

        public async Task UpdateAsync(Deal deal)
        {
            _context.Deals.Update(deal);
        }

    }
}
