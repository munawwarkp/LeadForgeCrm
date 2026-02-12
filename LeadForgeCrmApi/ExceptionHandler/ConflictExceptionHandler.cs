using LeadForgeCrm.Application.Exceptions;
using Microsoft.AspNetCore.Diagnostics;

namespace LeadForgeCrm.Api.ExceptionHandler
{
    public class ConflictExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext, 
            Exception exception, 
            CancellationToken cancellationToken)
        {
            if (exception is not ConflictException conflict)
                return false;

            httpContext.Response.StatusCode = StatusCodes.Status409Conflict;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = conflict.Message
            }, cancellationToken);

            return true;
        }
    }
}
