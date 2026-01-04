using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LeadForgeCrm.Infrastructure.Tenancy
{
    public class TenantProvider : ITenantProvider
    {
        public int TenantId { get; }

        public TenantProvider(IHttpContextAccessor accessor)
        {
            var claim = accessor.HttpContext?.User?.FindFirst("TenantId");

            TenantId = claim != null ?int.Parse(claim.Value) : 0;
        }
    }
}
