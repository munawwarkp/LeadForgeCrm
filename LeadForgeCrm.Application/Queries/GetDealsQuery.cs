using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Queries
{
    public record GetDealsQuery(
        int pageNumber = 1,
        int pageSize = 20,
        string? SearchTerm = null,
        StageStatus? StageStatus = null,
        int? AssignedUserId = null
        ) : IRequest<PagedResult<DealBoardDto>>;

    public class GetDealsQueryHandler(IDealRepository dealRepository) 
        : IRequestHandler<GetDealsQuery, PagedResult<DealBoardDto>>
    {
        public async Task<PagedResult<DealBoardDto>> Handle(GetDealsQuery request, CancellationToken cancellationToken)
        {
            var (deals, totalCount) = await dealRepository.GetDealsAsync(
                request.pageNumber,
                request.pageSize,
                request.SearchTerm,
                request.StageStatus,
                request.AssignedUserId,
                cancellationToken
                );

            return new PagedResult<DealBoardDto>
            {
                Items = deals.Select(d => new DealBoardDto
                {
                    Id = d.Id,
                    Title = d.Title,
                    ContactName = d.Contact != null ? d.Contact.FirstName + " " + d.Contact.LastName : string.Empty,
                    CompanyName = d.Company?.Name ?? string.Empty,
                    StageId = d.PipelineStageId,
                    Amount = d.Amount,
                    Description = d.Description,
                }).ToList(),

                TotalCount = totalCount,
                PageNumber = request.pageNumber,
                PageSize = request.pageSize
            };

          
        }

    }
}