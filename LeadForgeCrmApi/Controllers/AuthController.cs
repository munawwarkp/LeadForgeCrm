using LeadForgeCrm.Api.Dtos.Requests;
using LeadForgeCrm.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator )
        {
            _mediator = mediator;
        }

        [HttpPost("Signup")]
        public async Task<IActionResult> Signup([FromBody]SignupRequest request)
        {
            var command = new SignupCommand(
                    request.FullName,
                    request.Email,
                    request.Password,
                    request.CompanyName,
                    request.PhoneNumber
                );

            var result = await _mediator.Send(command);
            return Ok(result);
        }



    }
}
