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
    public record GetRolesQuery() : IRequest<List<RoleDto>>;
    public class GetRolesQueryHandler(
        IRoleRepository roleRepository,
        ILogger<GetRolesQueryHandler> logger
        ) : IRequestHandler<GetRolesQuery, List<RoleDto>>
    {
        public async Task<List<RoleDto>> Handle(GetRolesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var roles = await roleRepository.GetRolesAsync();

                var roleDtos = roles.Select(r => new RoleDto
                {
                    Id = r.Id,
                    Name = r.Name,
                });

                return roleDtos.ToList();

            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error occurred while retrieving roles.");
                throw;
            }
        }
    }
}
