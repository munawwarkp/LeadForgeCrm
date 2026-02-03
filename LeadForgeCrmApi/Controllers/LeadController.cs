using System.Data;
using System.Formats.Asn1;
using LeadForgeCrm.Api.Contracts;
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
    public class LeadController : ControllerBase
    {
        private readonly IMediator _mediator;
        public LeadController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [Authorize]
        [HttpPost("CreateLead")]
        public async Task<IActionResult> CreateLead([FromBody]LeadRequest request)
        {
            var command = new CreateLeadCommand(
                    request.Name,
                    request.Email,
                    request.Phone,
                    request.LeadSource
                );
            var result = await _mediator.Send(command);
            return Ok(result);
        }

        [HttpPatch("{leadId:int}/status")]
        public async Task<IActionResult> UpdateLeadStatus(int leadId,UpdateLeadStatusRequest request)
        {
            var res = await _mediator.Send(new UpdateLeadStatusCommand(leadId, request.Status));
            return Ok(res);
        }

        [HttpDelete("{leadId:int}")]
        public async Task<IActionResult> DeleteLead(int leadId)
        {
            var command = new DeleteLeadCommand(leadId);
            await _mediator.Send(command);
            return NoContent();
        }

        [HttpGet]
        public async Task<IActionResult> GetLeads([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 20)
        {
            var query = new GetLeadsQuery(pageNumber, pageSize);

            var leads = await _mediator.Send(query);
            return Ok(leads);
        }

        [HttpGet("{leadId:int}")]
        public async Task<IActionResult> GetLeadById(int leadId)
        {
            var query = new GetLeadByIdQuery(leadId);
            var lead = await _mediator.Send(query);
            return Ok(lead);
        }
    }
}
