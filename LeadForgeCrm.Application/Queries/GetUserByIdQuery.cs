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
    public record GetUserByIdQuery(
        int Id
        ): IRequest<UserResponse>;

    public class GetUserByIdQueryHandler(
        IUserRepository userRepository,
        ILogger<GetUserByIdQueryHandler> logger
        ) : IRequestHandler<GetUserByIdQuery, UserResponse>
    {
        public async Task<UserResponse> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await userRepository.GetByIdAsync(request.Id);

                return new UserResponse
                {
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Role = user.Role.Name,
                    CreatedAt = user.CreatedAt,
                };

            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
