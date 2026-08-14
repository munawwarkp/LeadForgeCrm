using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Constants;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    public record CreateLeadCommand(
        string Name,
        string Email,
        string Phone,
        string LeadSource
        ) :IRequest<Result<int>>;

    public class CreateLeadCommandHandler(
        ITenantProvider tenantProvider,
        ILeadRepository leadRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork,
        IUserProvider userProvider) : IRequestHandler<CreateLeadCommand, Result<int>>
    {
        public async Task<Result<int>> Handle(CreateLeadCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var tenantId = tenantProvider.TenantId;
                if(tenantId == 0)
                {
                    return Result<int>.Fail("Tenant not found");
                }

                //prevent duplicate leads
                var duplicate = await leadRepository.ExistsByEmailAsync(request.Email, tenantId, cancellationToken);
                if(duplicate)
                    return Result<int>.Fail("Lead with the same email already exists.");

                //Create lead
                var lead = new Lead
                {                   
                    Name = request.Name,
                    Email = request.Email,
                    Phone = request.Phone,
                    LeadSource = request.LeadSource,
                    Status = LeadStatuses.New,
                    TenantId = tenantId,
                    CreatedAt = DateTime.UtcNow,
                    CreatedByUserId = userProvider.UserId
                };

                //// if logged in user created it, assign automatically
                //if (request.LeadSource != "meta")
                //{
                //    lead.AssignedToId = userProvider.UserId;
                //}

                await leadRepository.AddLead(lead);
                await unitOfWork.SaveChangesAsync(cancellationToken);

                return Result<int>.Ok(lead.Id);
            }
            catch (Exception ex)
            {
                return Result<int>.Fail(ex.Message);
            }
        }

    }
  
}
