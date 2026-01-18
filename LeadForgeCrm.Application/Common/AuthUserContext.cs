using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Common
{
    public record AuthUserContext(
        int UserId,
        string Email,
        string Role,
        int TenantId);


}
