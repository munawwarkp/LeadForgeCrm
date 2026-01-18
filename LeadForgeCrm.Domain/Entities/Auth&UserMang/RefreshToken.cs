using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Base;

namespace LeadForgeCrm.Domain.Entities.Auth_UserMang
{
    public class RefreshToken:BaseTenantEntity
    {
        public int UserId { get; set; }
        public User User { get; set; }


        public string Token { get; set; }
        public DateTime ExpiresAt { get; set; }

        public DateTime? RevokedAt { get; set; }
        //public string? ReplacedByToken { get; set; }


        public bool IsExpired => DateTime.UtcNow >= ExpiresAt;
        public bool IsActive => RevokedAt == null && !IsExpired;
    }
}
