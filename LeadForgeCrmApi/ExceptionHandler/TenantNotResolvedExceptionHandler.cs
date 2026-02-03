using Microsoft.AspNetCore.Diagnostics;

namespace LeadForgeCrm.Api.ExceptionHandler
{
    public class TenantNotResolvedExceptionHandler : IExceptionHandler
    {
        public async ValueTask<bool> TryHandleAsync(
            HttpContext httpContext,
            Exception exception,
            CancellationToken cancellationToken)
        {
            if (exception is not TenantNotResolvedException)
                return false;

            httpContext.Response.StatusCode = StatusCodes.Status400BadRequest;

            await httpContext.Response.WriteAsJsonAsync(new
            {
                error = "Tenant not resolved",
                message = "Tenant information is missing or invalid"
            }, cancellationToken);
            return true;
        }
    }
}
