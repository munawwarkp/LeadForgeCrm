using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Queries
{
    public record GetLeadByIdQuery(
        int leadId): IRequest<Result<LeadListItemDto>>;

    public class GetLeadByIdQueryHandler(
        ILeadRepository leadRepository,
        ILogger<GetLeadByIdQueryHandler> logger) : IRequestHandler<GetLeadByIdQuery, Result<LeadListItemDto>>
    {
        public async Task<Result<LeadListItemDto>> Handle(GetLeadByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await leadRepository.GetLeadByIdAsync(request.leadId, cancellationToken);

                if (res is null)
                {
                    logger.LogWarning(
                        "Lead not found. LeadId: {LeadId}",
                        request.leadId);

                   return Result<LeadListItemDto>.Fail("Lead not found");
                }

                return Result<LeadListItemDto>.Ok(
                    new LeadListItemDto(
                                res.Id,
                                res.Name,
                                res.Email,
                                res.Phone,
                                res.LeadSource,
                                res.Status,
                                res.CreatedByUser?.FirstName ?? "Unassigned",
                                res.CreatedAt
                                ));
            }
            catch(Exception ex)
            {
                logger.LogError(ex,
                    "Error retrieving lead by Id: {LeadId}",
                    request.leadId);
                throw;
            }
        }
    }


}
