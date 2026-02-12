using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Repositories
{
    public class ContactRepository : IContactRepository
    {
        private readonly AppDbContext _context;
        public ContactRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact contact,CancellationToken ct)
        {
            await _context.Contacts.AddAsync(contact,ct);
            
        }

        public async Task<bool> ExistAsync(string email)
        {
            return await _context.Contacts.AnyAsync(c => c.Email == email);
        }
    }
}
