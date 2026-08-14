using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    public record DeactivateUserCommand(
        int Id
        ): IRequest<bool>;
    public class DeactivateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<DeactivateUserCommand, bool>
    {
        public async Task<bool> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if(user == null)
            {
                throw new KeyNotFoundException("User not found.");
            }

            user.IsActive = false;

            await unitOfWork.SaveChangesAsync(cancellationToken);
            return true;

        }
    }
}
