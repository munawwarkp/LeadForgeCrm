using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class UserRepository:IUserRepository
    {
        private readonly AppDbContext _context;
        public UserRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<User?> GetEmailAsync(string email)
        {
            return await _context.Users
                    .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            _context.Users .Add(user);
            await _context.SaveChangesAsync();
        }
    }
}
