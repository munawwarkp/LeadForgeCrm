using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Commands
{
    public record DeleteLeadCommand(
        int leadId) : IRequest;

    public class DeleteLeadCommandHandler(
        ILeadRepository leadRepository,
        IUnitOfWork unitOfWork,
        ILogger<DeleteLeadCommand> logger) : IRequestHandler<DeleteLeadCommand>
    {
        public async Task Handle(DeleteLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                await leadRepository.DeleteAsync(request.leadId);
                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error deleting lead with ID {LeadId}", request.leadId);
            }
        }
    }


}
