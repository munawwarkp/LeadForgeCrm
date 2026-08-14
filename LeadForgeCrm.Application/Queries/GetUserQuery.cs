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
    public record GetUserQuery: IRequest<List<UserResponse>>;

    public class GetUserQueryHandler(
        IUserRepository userRepository,
        ILogger<GetUserQueryHandler> logger) : IRequestHandler<GetUserQuery, List<UserResponse>>
    {
        public async Task<List<UserResponse>> Handle(GetUserQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var users = await userRepository.GetUsers();

                return  users.Select(u => new UserResponse
                    {
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        Email = u.Email,
                        Role = u.Role.Name,
                        CreatedAt = u.CreatedAt
                    }).ToList();
                
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
