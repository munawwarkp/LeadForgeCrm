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
    public record UpdateDealCommand(
        int Id,
        string ? Title,
        decimal? Amount,
        DateTime? ExpectedCloseDate,
        int? Probability,
        string ? Description) : IRequest;

    public class UpdateDealCommandHandler(
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork,
        ILogger<UpdateDealCommand> logger) : IRequestHandler<UpdateDealCommand>
    {
        public async Task Handle(UpdateDealCommand request, CancellationToken ct)
        {
            try
            {
                var deal = await dealRepository.GetByIdAsync(request.Id, ct);

                if (deal == null)
                {
                    throw new KeyNotFoundException($"Deal with Id {request.Id} not found.");
                }

                deal.Title = request.Title ?? deal.Title;
                deal.Amount = request.Amount ?? deal.Amount;
                deal.ExpectedCloseDate = request.ExpectedCloseDate ?? deal.ExpectedCloseDate;
                deal.Probability = request.Probability ?? deal.Probability;
                deal.Description = request.Description ?? deal.Description;

                await unitOfWork.SaveChangesAsync(ct);
            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                throw;  
            }
           
        }
    }
}
