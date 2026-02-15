using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Exceptions;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    public record CreateCompanyCommand(string Name, string? Address, string? Phone) : IRequest<CompanyResponse>;

    public class CreateCompanyCommandHandler : IRequestHandler<CreateCompanyCommand, CompanyResponse>
    {
        private readonly ICompanyRepository _companyRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITenantProvider _tenantProvider;
        public CreateCompanyCommandHandler(ICompanyRepository companyRepository, IUnitOfWork unitOfWork,ITenantProvider tenantProvider)
        {
            _companyRepository = companyRepository;
            _unitOfWork = unitOfWork;
            _tenantProvider = tenantProvider;
        }
        public async Task<CompanyResponse> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
        {
            if(await _companyRepository.ExistsByNameAsync(request.Name, cancellationToken))
            {
                throw new ConflictException($"A company with the name '{request.Name}' already exists.");
            }

            var company = new Company
            {
                TenantId = _tenantProvider.TenantId,
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                WebSiteUrl = null,
                CreatedAt = DateTime.UtcNow
            };           
            await _companyRepository.AddAsync(company,cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return new CompanyResponse(
                company.Id,
                company.Name,
                company.Address,
                company.Phone
            );
        }

    }
}