using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LeadForgeCrm.Infrastructure.Tenancy
{
    public class TenantProvider : ITenantProvider
    {
        private readonly IHttpContextAccessor _accessor;


        public TenantProvider(IHttpContextAccessor accessor)
        {
            _accessor = accessor;
        }
        public int TenantId =>
           int.Parse( _accessor.HttpContext?.Items["TenantId"]?.ToString() 
                ?? throw new Exception("Tenant not resolved"));

    }
}
