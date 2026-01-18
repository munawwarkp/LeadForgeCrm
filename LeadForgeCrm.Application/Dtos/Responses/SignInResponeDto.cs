using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Dtos.Responses
{
    public class SignInResponeDto
    {
        public int Id { get; set; }
        public int TenantId { get; set; }
        public string FullName { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string AccessToken { get; init; } = default!;
        public string RefreshToken { get; init; } = default!;
        public DateTime ExpiresAt { get; init; }

    }
}
