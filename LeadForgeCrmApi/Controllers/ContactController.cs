using LeadForgeCrm.Application.Commands;
using LeadForgeCrm.Application.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateContact(CreateContactRequest request)
        {
            var command = new CreateContactCommand(
                request.FirstName,
                request.LastName,
                request.Email,
                request.Phone,
                request.CompanyId);

            var result = await _mediator.Send(command);

            return Ok(result);
        }
    }
}
