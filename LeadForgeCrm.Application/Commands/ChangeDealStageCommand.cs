using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    //later clean code
    public record ChangeDealStageCommand(
        int DealId,
        int Order):IRequest<Result<DealChangeResponse>>;

    public class changeDealStageCommandHandler (
        IDealRepository dealRepository,
        IPipelineStageRepository stageRepository,
        IUnitOfWork unitOfWork): IRequestHandler<ChangeDealStageCommand, Result<DealChangeResponse>>
    {
        public async Task<Result<DealChangeResponse>> Handle(ChangeDealStageCommand request, CancellationToken cancellationToken)
        {
            //check deal exist
            var deal = await dealRepository.GetByIdAsync(request.DealId, cancellationToken);
            if(deal == null)
                return Result<DealChangeResponse>.Fail("Deal not found");

            //order - stage id found
            var nextStage = await stageRepository.GetNextStageAsync(deal.PipelineId, request.Order);
            
            if(nextStage == null)
                return Result<DealChangeResponse>.Fail("No next stage found for the given order");

            deal.ChangeStage(nextStage.Id, request.Order);
            deal.CreatedAt = DateTime.UtcNow;
            deal.Probability = nextStage.DeafultProbability;

            await dealRepository.UpdateAsync(deal);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var response = new DealChangeResponse(
                DealId: deal.Id,
                PipelineStageId: nextStage.Id,
                Order: request.Order,
                Title: deal.Title,
                Amount: deal.Amount,
                ExpectedCloseDate: deal.ExpectedCloseDate,
                Status: deal.Status.ToString()
            );
            return Result<DealChangeResponse>.Ok(response);
        }
    }
}
