using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Queries
{
    public record GetLeadsQuery(
        int PageNumber,
        int PageSize = 20) : IRequest<IReadOnlyList<LeadListItemDto>>;

    public class GetLeadsQueryHandler(
        ILeadRepository leadRepository,
        ILogger<GetLeadsQueryHandler> logger) : IRequestHandler<GetLeadsQuery, IReadOnlyList<LeadListItemDto>>
    {
        public async Task<IReadOnlyList<LeadListItemDto>> Handle(GetLeadsQuery request, CancellationToken cancellationToken)
        {
            var pageNumber = request.PageNumber < 1 ? 1 : request.PageNumber;
            var pageSize = request.PageSize < 1 ? 20 : request.PageSize;
            try
            {
                var leads = await leadRepository.GetLeadsAsync(pageNumber, pageSize, cancellationToken);

             
                var res = leads.Select(l => new LeadListItemDto(
                            l.Id,
                            l.Name,
                            l.Email,
                            l.Phone,
                            l.LeadSource,
                            l.Status,
                            l.AssignedTo?.FullName ?? "Unassigned",
                            l.CreatedAt
                            )).ToList();

                return res;
            }
            catch(Exception ex)
            {
                logger.LogError(
                 ex,
                 "Error retrieving leads. PageNumber: {PageNumber}, PageSize: {PageSize}",
                 pageNumber,
                 pageSize);

                throw;
            }
        }
    }

}
