using LeadForgeCrm.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
    }
}
