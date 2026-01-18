using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.Auth_UserMang;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class RefreshTokenRespository:IRefreshTokenRepository
    {
        private readonly AppDbContext _context;
        public RefreshTokenRespository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddRefreshTokenAsync(RefreshToken tokenEntity)
        {
            await _context.RefreshTokens.AddAsync(tokenEntity);
            //await _context.SaveChangesAsync();
        }

        public async Task<RefreshToken?> GetRefreshTokenAsync(string token)
        {
            return await _context.RefreshTokens
                .IgnoreQueryFilters()
                .FirstOrDefaultAsync(rt => rt.Token == token);
        }

        public void Update(RefreshToken token)
        {
            _context.RefreshTokens.Update(token);
            //_context.SaveChanges();
        }
    }
}
