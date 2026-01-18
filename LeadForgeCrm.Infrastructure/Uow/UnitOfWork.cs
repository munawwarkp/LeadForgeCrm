using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace LeadForgeCrm.Infrastructure.Uow
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        //private IDbContextTransaction? _transaction;
        public UnitOfWork(AppDbContext context) {
            _context = context;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return _context.SaveChangesAsync(cancellationToken);
        }

        //public async Task BeginTransactionAsync(CancellationToken ct = default)
        //{
        //    if (_transaction != null)
        //        return;

        //    _transaction = await _context.Database.BeginTransactionAsync(ct);
        //}

        //public async Task CommitAsync(CancellationToken ct = default)
        //{
        //    if (_transaction == null) throw new InvalidOperationException("No active transaction.");

        //    await _context.SaveChangesAsync(ct);   // commit all changes tracked by repositories
        //    await _transaction.CommitAsync(ct);
        //    await _transaction.DisposeAsync();
        //    _transaction = null;
        //}

        //public async Task RollbackAsync(CancellationToken ct = default)
        //{
        //    if (_transaction == null) return;
        //    await _transaction.RollbackAsync(ct);
        //    await _transaction.DisposeAsync();
        //    _transaction = null;
        //}
    }
}
