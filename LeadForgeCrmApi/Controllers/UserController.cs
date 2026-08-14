using LeadForgeCrm.Application.Commands;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController(IMediator mediator, ILogger<UserController> logger) : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var res = await mediator.Send(
                new GetUserQuery());

            return Ok(res);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetById(int id)
        {
            var res = await mediator.Send(  
                new GetUserByIdQuery(id)
                );

            return Ok(res); 
        }

        [HttpPost]
        public async Task<IActionResult> Create(UserRequest request)
        {
            var res = await mediator.Send(
                new CreateUserCommand(request)
                );

            if (!res)
            {
                return BadRequest();
            }

            return NoContent();

        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Update(int id, UserUpdateRequest request)
        {
            try
            {
                var res = await mediator.Send(
                  new UpdateUserCommand(id, request)
                  );

                return Ok(new
                {
                    message = "User updated successfully",
                });
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error occurred while updating user {UserId}", id);

                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
          
        }

        [HttpPatch("{id:int}/deactivate")]
        public async Task<IActionResult> Deactivate(int id)
        {
            try
            {
                var res = await mediator.Send(
                    new DeactivateUserCommand(id)
                    );

                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Error occurred while deactivating user {UserId}", id);
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }

        [HttpPatch("{id:int}/activate")]
        public async Task<IActionResult> Activate(int id)
        {
            try
            {
                var res = await mediator.Send(
                    new ActivateUsercommand(id)
                    );

                return NoContent();
            }
            catch(KeyNotFoundException ex)
            {
                return NotFound(new
                {
                    message = ex.Message
                });
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error occurred while activating user {UserId}", id);
                return StatusCode(500, new
                {
                    message = "An unexpected error occurred."
                });
            }
        }

    }
}
