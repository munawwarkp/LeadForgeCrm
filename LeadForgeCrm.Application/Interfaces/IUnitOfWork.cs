using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LeadForgeCrm.Application.Interfaces
{
    public interface IUnitOfWork
    {

        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        //Task BeginTransactionAsync(CancellationToken ct = default);
        //Task CommitAsync(CancellationToken ct = default);
        //Task RollbackAsync(CancellationToken ct = default);
    }

}
