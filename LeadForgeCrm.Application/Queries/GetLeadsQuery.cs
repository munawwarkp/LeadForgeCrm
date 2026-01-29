using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Responses;
using MediatR;

namespace LeadForgeCrm.Application.Queries
{
    public record GetLeadsQuery(
        int PageNumber = 1,
        int PageSize = 20) : IRequest<IReadOnlyList<LeadListItemDto>>;

    public class GetLeadsQueryHandler() : IRequestHandler<GetLeadsQuery, IReadOnlyList<LeadListItemDto>>
    {
        public async Task<IReadOnlyList<LeadListItemDto>> Handle(GetLeadsQuery request, CancellationToken cancellationToken)
        {
            // Implementation to retrieve leads would go here.
            // This is a placeholder to illustrate the structure.
            return new List<LeadListItemDto>();
        }
    }

}
