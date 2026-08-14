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
    public record ActivateUsercommand(
        int Id
        ): IRequest<bool>;
    public class ActivateUserCommandHandler(
        IUserRepository userRepository,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<ActivateUsercommand, bool>
    {
        public async Task<bool> Handle(ActivateUsercommand request, CancellationToken cancellationToken)
        {
            var user = await userRepository.GetByIdAsync(request.Id);

            if (user is null)
            {
                throw new KeyNotFoundException("User not found");
            }

            user.IsActive = true;   

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return true;

        }
    }
}
