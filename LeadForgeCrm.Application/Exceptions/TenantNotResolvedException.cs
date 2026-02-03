namespace LeadForgeCrm.Api.ExceptionHandler
{
    public class TenantNotResolvedException :Exception
    {
        public TenantNotResolvedException() : base("Tenant not resolved") {}
    }
}
