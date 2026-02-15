using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Requests
{
    public record CreateCompanyRequest(
        string Name,
        string? Phone,
        string? Address,
        string? WebSiteUrl
        );
   
}
