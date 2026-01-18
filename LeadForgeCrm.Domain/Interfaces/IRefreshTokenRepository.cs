using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Auth_UserMang;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IRefreshTokenRepository
    {
        Task AddRefreshTokenAsync(RefreshToken tokenEntity);
        Task<RefreshToken?> GetRefreshTokenAsync(string token);
        void Update(RefreshToken token);
    }

}
