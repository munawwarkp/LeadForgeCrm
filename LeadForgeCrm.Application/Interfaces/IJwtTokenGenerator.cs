using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Domain.Entities;

namespace LeadForgeCrm.Application.Interfaces
{
    public interface IJwtTokenGenerator
    {
        string GenerateAccessToken(AuthUserContext user, out DateTime expiresAt);
        string GenerateRefreshToken();
    }
}
