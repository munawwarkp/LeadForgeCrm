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
    public record DeleteDealCommand(int id): IRequest;

    public class DeleteDealCommandHandler(
        IDealRepository dealRepository,
        ILogger<DeleteDealCommandHandler> logger,
        IUnitOfWork unitOfWork) : IRequestHandler<DeleteDealCommand>
    {
        public async Task Handle(DeleteDealCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var deal = await dealRepository.GetByIdAsync(request.id, cancellationToken)
                    ?? throw new KeyNotFoundException("Deal not found");

                if(deal.IsDeleted)
                    throw new InvalidOperationException("Deal is already deleted");


                deal.IsDeleted = true;
                deal.DeletedAt = DateTime.UtcNow;

                await unitOfWork.SaveChangesAsync(cancellationToken);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
        }
    }
}
