using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Commands
{
    public record CreateUserCommand(
        UserRequest UserRequest
        ): IRequest<bool>;

    public class CreateUserCommandHandler(
        IRoleRepository roleRepository,
        IUserRepository usereRepository,
        ILogger<CreateUserCommandHandler> logger,
        ITenantProvider tenantProvider,
        IPasswordHasher<User> passwordHasher,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateUserCommand, bool>
    {
        public async Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenantId = tenantProvider.TenantId;

                //tenant id return false


                //check the entered role is avaiable for this tenant
                var role = await roleRepository.GetRoleById(request.UserRequest.RoleId);

                if(role == null || role.TenantId != tenantId)
                {
                    throw new UnauthorizedAccessException("Invalid role.");
                }

                var user = new User
                {
                    TenantId = tenantId,
                    FirstName = request.UserRequest.FirstName,
                    LastName = request.UserRequest.LastName,
                    Email = request.UserRequest.Email,
                    Role = role,
                    CreatedAt = DateTime.UtcNow,
                };

                user.PasswordHash = passwordHasher.HashPassword(user, request.UserRequest.Password);
                usereRepository.Add(user);

                await unitOfWork.SaveChangesAsync(cancellationToken);
                return true;
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message, ex);
                throw;
            }
        }
    }

}
