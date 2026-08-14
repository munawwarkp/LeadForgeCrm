using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    //later change
    public record CreateDealCommand(
        DealRequest request) : IRequest<Result<CreateDealResponse>>;

    public class CreateDealCommandHandler(
        IDealRepository dealRepository,
        ITenantProvider tenantProvider,
        IPipelineRepository pipelineRepository,
        IPipelineStageRepository pipelineStageRepository,
        IUnitOfWork unitOfWork,
        IUserProvider userProvider) : IRequestHandler<CreateDealCommand, Result<CreateDealResponse>>
    {
        public async Task<Result<CreateDealResponse>> Handle(CreateDealCommand request, CancellationToken cancellationToken)
        {
            var defaultPipeline = await pipelineRepository.GetDefaultPipelineAsync();
            //order be pipeline stage order

            var stage = await pipelineStageRepository.GetByIdAsync(request.request.StageId, cancellationToken);
            if (stage == null)
            {
                throw new KeyNotFoundException($"Pipeline stage with id {request.request.StageId} not found.");
            }


            var amount = request.request.Amount ?? 0m;
            var deal = new Deal
            {
                TenantId = tenantProvider.TenantId,
                PipelineId = defaultPipeline.Id,
                Title = request.request.DealName,
                CompanyId = request.request.CompanyId,
                ContactId = request.request.ContactId,
                PipelineStageId = stage.Id,
                Order = stage.Order,
                Amount = amount,
                Probability = stage.DeafultProbability,
                ExpectedCloseDate = request.request.ClosingDate,
                Description = request.request.Description ?? string.Empty,
                CreatedAt = DateTime.UtcNow
                //CreatedByUserId = userProvider.UserId
            };

            await dealRepository.AddAsync(deal);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var res = new CreateDealResponse(
                Title: deal.Title,
                Amount: deal.Amount,
                ClosingDate: deal.ExpectedCloseDate,
                Description: deal.Description
            );
            return Result<CreateDealResponse>.Ok(res);
        }
    }
}
