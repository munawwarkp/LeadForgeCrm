using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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
    public class ActivityController(IMediator mediator) : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> Create(ActivityRequest request, CancellationToken ct)
        {
            //change user id here - clean architecture maintain - later
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim is null)
                return Unauthorized();

            if (!int.TryParse(userIdClaim.Value, out var userId))
                return Unauthorized();

            await mediator.Send(
                new CreateActivityCommand(userId, request),
                ct);


            return Ok();
        }

        //eg: get all activiteis belongs to lead(entity) of  --- id
        [HttpGet]
        public async Task<IActionResult> GetActivities(
            [FromQuery] GetActivitiesQuery query,
            CancellationToken ct
            )
        {
           var res =  await mediator.Send(
                new GetActivitiesQuery(
                    query.EntityType,
                    query.EntityId
                    
                ), ct);

            return Ok(res); 
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(
             int id,
             CancellationToken ct)
        {
            var res = await mediator.Send(
                new GetActivityByIdQuery(id)
                );

            return Ok(res);
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(
            int id,
            UpdateActivityRequest request,
            CancellationToken ct
            )
        {
            var updated = await mediator.Send(
                new UpdateActivityCommand(id, request),
                ct
                );

            if(!updated)
                return NotFound();

            return NoContent();
        }


        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(
            int id,
            CancellationToken ct)
        {
            var deleted = await mediator.Send(
                new DeleteActivityCommand(id),
                ct
                );

            if(!deleted)
                return NotFound();

            return NoContent();
        }
    }
}
