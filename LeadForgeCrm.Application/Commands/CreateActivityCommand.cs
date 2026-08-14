using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Commands
{
    public record CreateActivityCommand(
        int UserId,
        ActivityRequest ActivityRequest
        ) : IRequest;

    public class CreateActivityCommandHandler(
        IActivityEntityValidator activityEntityValidator,
        IActivityRepository activityRepository,
        ILogger<CreateActivityCommandHandler> logger,
        ITenantProvider tenantProvider,
        IUnitOfWork unitOfWork
        ) : IRequestHandler<CreateActivityCommand>
    {
        public async Task Handle(CreateActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var isExists = await activityEntityValidator.ValidateAsync(
                         request.ActivityRequest.EntityType,
                         request.ActivityRequest.EntityId,
                         cancellationToken
                         );

                if (!isExists)
                    throw new ArgumentException($"The entity of type {request.ActivityRequest.EntityType} with ID {request.ActivityRequest.EntityId} does not exist.");

                var activity = new Activity
                {
                    AssignedUserId = request.UserId,
                    TenantId = tenantProvider.TenantId,
                    Type = request.ActivityRequest.Type,
                    Description = request.ActivityRequest.Description,
                    ActivityDate = request.ActivityRequest.ActivityDate,
                    EntityId = request.ActivityRequest.EntityId,
                    EntityType = request.ActivityRequest.EntityType,
                };

                await activityRepository.AddAsync(activity, cancellationToken);
                await unitOfWork.SaveChangesAsync(cancellationToken);

            }
            catch (Exception ex)
            {
                logger.LogError(ex, "An error occurred while creating an activity.");
                throw;
            }
         
        }
    }

}
