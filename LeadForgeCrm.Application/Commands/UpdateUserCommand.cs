using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Commands
{
    public record UpdateUserCommand(
        int Id,
       UserUpdateRequest UserUpdateRequest
    ) : IRequest<bool>;
    public class UpdateUserCommandHandler(
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        ILogger<UpdateUserCommandHandler> logger,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateUserCommand, bool>
    {
        public async Task<bool> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if (user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            var role = await roleRepository.GetRoleById(request.UserUpdateRequest.RoleId);
            if (role == null)
            {
                throw new KeyNotFoundException("Role not found.");
            }

            user.FirstName = request.UserUpdateRequest.FirstName;
            user.LastName = request.UserUpdateRequest.LastName;
            user.UpdatedAt = DateTime.UtcNow;
            user.RoleId = request.UserUpdateRequest.RoleId;

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}
