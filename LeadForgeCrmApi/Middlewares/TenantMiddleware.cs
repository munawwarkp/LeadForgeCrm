namespace LeadForgeCrm.Api.Middlewares
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate _next;

        public TenantMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task InvokeAsync(HttpContext context)
        {

            if (!context.User.Identity?.IsAuthenticated ?? true)
            {
                await _next(context);
                return;
            }

            var tenantId = int.Parse( 
                context.User.FindFirst("TenantId")!.Value
                );

            context.Items["TenantId"] = tenantId;

            await _next(context);
        }
    }
}
