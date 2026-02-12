using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.XPath;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Exceptions;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    public record CreateContactCommand(
        string FirstName,
        string LastName,
        string Email,
        string ?Phone,
        int? CompanyId
        ) : IRequest<Result<ContactResponse>>;

    public class CreateContactCommandHandler(
        IContactRepository contactRepository,
        IUnitOfWork unitOfWork,
        ITenantProvider tenantProvider,
        IUserProvider userProvider,
        ICompanyRepository companyRepository) : IRequestHandler<CreateContactCommand,Result<ContactResponse>>
    {
        public async Task<Result<ContactResponse>> Handle(CreateContactCommand request, CancellationToken cancellationToken)
        {
            int? companyId = request.CompanyId;

            if (companyId.HasValue)
            {
                var exists = await companyRepository.ExistsAsync(companyId.Value, cancellationToken);

                if (!exists)
                    companyId = null;   // ← silently fallback to null
            }


            var emailExists = await contactRepository.ExistAsync(request.Email);
            if (emailExists)
                throw new ConflictException("Email already exists.");


            var contact = new Contact
            {
                TenantId = tenantProvider.TenantId,
                FirstName = request.FirstName,
                LastName = request.LastName,
                CompanyId = companyId,
                Email = request.Email,
                Phone = request.Phone,
                OwnerId = userProvider.UserId,
                CreatedAt = DateTime.UtcNow 
            };

            await contactRepository.AddAsync(contact, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);

            var res = new ContactResponse(
                Id: contact.Id,
                FirstName: contact.FirstName,
                LastName: contact.LastName,
                CompanyId: contact.CompanyId,
                Email: contact.Email,
                Phone: contact.Phone
            );
            return Result<ContactResponse>.Ok(res);

        }
    }

}
