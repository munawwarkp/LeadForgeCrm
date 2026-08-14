using LeadForgeCrm.Application.Commands;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class DealController : ControllerBase
    {
        private readonly IMediator _mediator;
        public DealController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPatch("{dealId}/stage")]
        public async Task<IActionResult> MoveDeal(int dealId, int order, CancellationToken ct)
        {
            var command = new ChangeDealStageCommand(dealId, order);
            var result = await _mediator.Send(command, ct);
            return Ok(result);

        }

        [HttpPost]
        public async Task<IActionResult> CreateDeal(DealRequest request, CancellationToken ct)
        {
            var command = new CreateDealCommand(request);
            var result = await _mediator.Send(command, ct);
            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] GetDealsQuery query,
            CancellationToken ct)
        {
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpGet("{dealId}")]
        public async Task<IActionResult> GetDealById(int dealId, CancellationToken ct)
        {
            var query = new GetDealByIdQuery(dealId);
            var result = await _mediator.Send(query, ct);
            return Ok(result);
        }

        [HttpPatch("{id}")]
        public async Task<IActionResult> Update(
                int id,
                UpdateDealRequest request,
                CancellationToken ct
            )
        {
            await _mediator.Send(
                
                new UpdateDealCommand(
                id,
                request.Title,
                request.Amount,
                request.ExpectedCloseDate,
                request.Probability,
                request.Description),
             ct);

            return NoContent();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id, CancellationToken ct)
        {
            var command = new DeleteDealCommand(id);
            await _mediator.Send(command, ct);
            return NoContent();
        }


    }
}
