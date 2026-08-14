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
    public record UpdateActivityCommand(
        int Id,
        UpdateActivityRequest UpdateActivityRequest
        ): IRequest<bool>;

    public class UpdateActivityCommandHandler(
        IActivityRepository activityRepository,
        ILogger<UpdateActivityCommandHandler> logger,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<UpdateActivityCommand, bool>
    {
        public async Task<bool> Handle(UpdateActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var activity = await activityRepository.GetActivityByIdAsync(request.Id, cancellationToken);

                if(activity is null)
                {
                    logger.LogWarning("Activity with Id {Id} not found", request.Id);
                    return false;
                }

                activity.Type = request.UpdateActivityRequest.Type;
                activity.Description = request.UpdateActivityRequest.Description;
                activity.ActivityDate = request.UpdateActivityRequest.ActivityDate;

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return true;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error updating activity with Id {Id}", request.Id);
                throw;
            }
        }
    }
}
