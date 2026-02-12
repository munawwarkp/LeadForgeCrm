using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Application.Services;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    public record UpdateLeadStatusCommand(
        int leadId,
        LeadStatus Status):IRequest<Result<UpdateLeadStatusResponse>>;


    public class UpdateLeadStatusCommandHandler(
        ILeadRepository leadRepository,
        IPipelineRepository pipelineRepository,
        IPipelineStageRepository pipelineStageRepository,
        IDealRepository dealRepository,
        IUnitOfWork unitOfWork) : IRequestHandler<UpdateLeadStatusCommand, Result<UpdateLeadStatusResponse>>
    {
        public async Task<Result<UpdateLeadStatusResponse>> Handle(UpdateLeadStatusCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var lead = await leadRepository.GetByIdAsync(request.leadId,cancellationToken);

                if (lead == null)
                    throw new Exception("Lead not found");

                lead.UpdateStatus(request.Status);

                if (request.Status == LeadStatus.Converted)
                {

                    //get default pipeline
                   var defaultPipeline = await pipelineRepository.GetDefaultPipelineAsync();
                    if (defaultPipeline == null)
                        throw new Exception("Default pipeline not configured");

                    var firstStage = await pipelineStageRepository.GetFirstStageAsync(defaultPipeline.Id);
                    if (firstStage == null)
                        throw new Exception("No stages found in the default pipeline");

                    var deal = new Deal
                    {
                        TenantId = lead.TenantId,
                        LeadId = lead.Id,
                        PipelineId = defaultPipeline.Id,
                        PipelineStageId = firstStage.Id,
                        Title = lead.Name,
                        Amount = 0,
                        Order = firstStage.Order,
                        CreatedAt = DateTime.UtcNow,    
                       
                    };

                    await dealRepository.AddAsync(deal);
                }



                await leadRepository.UpdateAsync(lead);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                var response = new UpdateLeadStatusResponse(
                    LeadId: lead.Id,
                    Status: request.Status
                );

                return Result<UpdateLeadStatusResponse>.Ok(response);
            }
            catch (Exception ex)
            {
                return Result<UpdateLeadStatusResponse>.Fail("Error updating lead status: " + ex.Message);
            }
        }
    }


}
