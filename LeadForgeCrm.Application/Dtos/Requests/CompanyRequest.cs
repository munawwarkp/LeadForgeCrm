using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Requests
{
    public record CompanyRequest(
        string CompanyName,
        string? Phone,
        string? WebsiteUrl);
  
}
