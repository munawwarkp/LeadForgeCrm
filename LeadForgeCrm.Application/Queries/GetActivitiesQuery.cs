using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Queries
{
    public record GetActivitiesQuery(
        ActivityEntityType EntityType,
        int EntityId
        ) : IRequest<List<ActivityResponse>>;

    public class GetActivitiesQueryHandler(
        IActivityRepository activityRepository,
        ILogger<GetActivitiesQueryHandler> logger   
        ) : IRequestHandler<GetActivitiesQuery, List<ActivityResponse>>
    {
        public async Task<List<ActivityResponse>> Handle(GetActivitiesQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var activities = await activityRepository.GetActivitiesByEntityAsync(request.EntityId,
              request.EntityType,
              cancellationToken
              );

                var activityResponse = activities.Select(a => new ActivityResponse
                {
                    Id = a.Id,
                    UserId = a.AssignedUserId,
                    Type = a.Type,
                    Description = a.Description,
                    ActivityDate = a.ActivityDate,
                    EntityId = a.EntityId,
                    EntityType = a.EntityType
                }).ToList();

                return activityResponse;
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "An error occurred while retrieving activities.");
                throw;
            }
          
        }
    }

}
