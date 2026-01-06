using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities;

namespace LeadForgeCrm.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetEmailAsync(string email);
        Task AddAsync(User user);

    }
}
