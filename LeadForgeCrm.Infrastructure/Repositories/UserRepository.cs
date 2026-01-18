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
               .IgnoreQueryFilters()
               .Include(u => u.Role)
               .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task<User?> GetByIdAsync(int id)
        {
            return await _context.Users
                .IgnoreQueryFilters()
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public void Add(User user)
        {
            _context.Users.Add(user);

            //move SaveChangesAsync to out of repositories-for unit of work pattern, so here repo, just track, no save
            //await _context.SaveChangesAsync();
        }
    }
}
