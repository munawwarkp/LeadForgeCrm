using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Queries
{
    public record GetActivityByIdQuery(
        int Id
        ): IRequest<ActivityResponse>;

    public class GetActivityByIdQueryHandler(
        IActivityRepository activityRepository,
        ILogger<GetActivityByIdQuery> logger
        ) : IRequestHandler<GetActivityByIdQuery, ActivityResponse>
    {
        public async Task<ActivityResponse> Handle(GetActivityByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var activity = await activityRepository.GetActivityByIdAsync(request.Id, cancellationToken);

                var activityResponse = new ActivityResponse
                {
                    Id = activity.Id,
                    Type = activity.Type,
                    Description = activity.Description,
                    ActivityDate = activity.ActivityDate,
                    CreatedAt = activity.CreatedAt,
                    EntityId = activity.EntityId,
                    EntityType = activity.EntityType,
                    UserId = activity.AssignedUserId,
                };

                return activityResponse;

            }
            catch(Exception ex)
            {
                logger.LogError(ex.Message);
                throw;
            }
          
        }
    }
}
