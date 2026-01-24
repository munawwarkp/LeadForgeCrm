using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LeadForgeCrm.Infrastructure.Tenancy
{
    public class UserProvider:IUserProvider
    {
        private readonly IHttpContextAccessor _httpContextAccessor;


        public UserProvider(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }


        public int UserId
        {
            get
            {
                var userIdClaim = _httpContextAccessor.HttpContext?.User?
                .FindFirst(JwtRegisteredClaimNames.Sub)?.Value;

                Console.WriteLine("user id from claim : "+userIdClaim);

                if (userIdClaim == null)
                    return 0;


                return int.Parse(userIdClaim);
            }
        }

    }
}
