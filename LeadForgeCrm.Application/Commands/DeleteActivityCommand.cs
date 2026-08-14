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
    public record DeleteActivityCommand(
        int Id
        ): IRequest<bool>;
    
    public class DeleteActivityCommandHandler: IRequestHandler<DeleteActivityCommand, bool>
    {
        private readonly IActivityRepository _activityRepository;
        private readonly IUnitOfWork _unitOfWork;
        private ILogger<DeleteActivityCommandHandler> _logger;
        public DeleteActivityCommandHandler(IActivityRepository activityRepository, IUnitOfWork unitOfWork, ILogger<DeleteActivityCommandHandler> logger)
        {
            _activityRepository = activityRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }
        public async Task<bool> Handle(DeleteActivityCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var activity = await _activityRepository.GetActivityByIdAsync(request.Id, cancellationToken);

                if(activity is null)
                {
                    return false;
                }

                activity.IsDeleted = true;

                await _unitOfWork.SaveChangesAsync(cancellationToken);

                return true;

            }
            catch(Exception ex)
            {
                _logger.LogError(ex.Message);
                throw;
            }
        }
    }

}
