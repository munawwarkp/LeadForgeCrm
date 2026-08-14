using LeadForgeCrm.Api.Common;
using LeadForgeCrm.Api.Dtos.Requests;
using LeadForgeCrm.Application.Commands;
using LeadForgeCrm.Application.Dtos.Requests;
using LeadForgeCrm.Application.Dtos.Responses;
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

        [HttpPost("Signin")]
        public async Task<IActionResult> SignIn([FromBody] LoginRequest request)
        {
           var command = new SigninCommand(
                request.Email,
                request.Password
                );

            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Error,
                    Data = null
                });
            }

            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Secure = true,
                SameSite = SameSiteMode.Strict,
                Expires = DateTime.UtcNow.AddDays(7)  //match the tokens lifetime
            };
                Response.Cookies.Append("refreshToken", result.Data!.RefreshToken, cookieOptions);


            return Ok(new ApiResponse<SignInResponeDto>
            {
                Success = true,
                Message = "Sign in successful",
                Data = result.Data
            });
        }

        [HttpPost("refresh")]
        public async Task<IActionResult> Refresh()
        {

            var refreshToken = Request.Cookies["refreshToken"];

            if (string.IsNullOrEmpty(refreshToken))
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = "Refresh token is missing.",
                    Data = null
                });
            }

            var command = new RefreshTokenCommand(refreshToken);
            
            var result = await _mediator.Send(command);

            if (!result.Success)
            {
                return Unauthorized(new ApiResponse<object>
                {
                    Success = false,
                    Message = result.Error,
                    Data = null
                });
            }
            return Ok(result);
        }

    }
}
