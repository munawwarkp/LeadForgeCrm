using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Queries
{
    public record GetDealByIdQuery(int dealId) : IRequest<Result<DealResponse>>;

    public class GetDealByIdQueryHandler(
        IDealRepository dealRepository,
        ILogger<GetDealByIdQueryHandler> logger 
        ) : IRequestHandler<GetDealByIdQuery,Result<DealResponse>>
    {
        public async Task<Result<DealResponse>> Handle(GetDealByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var res = await dealRepository.GetByIdAsync(request.dealId, cancellationToken);

                if (res is null)
                    return Result<DealResponse>.Fail("Deal not found");

                return Result<DealResponse>.Ok(new DealResponse
                {
                    Id = res.Id,
                    Title = res.Title,
                    Amount = res.Amount,
                    ExpectedRevenue = res.ExpectedRevenue,
                    ExpectedCloseDate = res.ExpectedCloseDate,
                    CreatedAt = res.CreatedAt,
                    Order = res.Order,
                    CompanyName = res.Company?.Name,
                    ContactName = res.Contact?.FirstName,
                    Probability = res.Probability,
                    Description = res.Description
                });
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error retrieving deal by Id: {DealId}", request.dealId);
                throw;
            }
        }
    }

}
