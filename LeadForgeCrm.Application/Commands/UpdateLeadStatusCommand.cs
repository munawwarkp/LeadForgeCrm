using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
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

                await leadRepository.UpdateAsync(lead);
                await unitOfWork.SaveChangesAsync();

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
