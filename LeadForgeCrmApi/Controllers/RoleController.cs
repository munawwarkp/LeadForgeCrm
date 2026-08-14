using LeadForgeCrm.Application.Queries;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController(
        IMediator mediator) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetRoles()
        {
            //tenantid missing return unauthorized, later add
            
            var roles = await mediator.Send(
                new GetRolesQuery());

            return Ok(roles);
        }
        //[HttpPost]
        //public async Task<IActionResult> CreateRole()
        //{
        //    // Implement logic to create a role
        //    return Ok(new { message = "CreateRole endpoint" });
        //}
    }
}
