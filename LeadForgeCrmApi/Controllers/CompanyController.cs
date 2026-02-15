using LeadForgeCrm.Application.Commands;
using LeadForgeCrm.Application.Dtos.Requests;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CompanyController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> CreateCompany([FromBody]CreateCompanyRequest request)
        {
            var command = new CreateCompanyCommand(
                request.Name,
                request.Phone,
                request.Address
            );
           var res = await _mediator.Send(command);
            return Ok(res);
        }
    }
}
