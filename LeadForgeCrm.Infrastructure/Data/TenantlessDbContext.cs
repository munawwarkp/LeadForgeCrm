using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Data
{
    public class TenantlessDbContext : DbContext
    {
        public TenantlessDbContext(DbContextOptions<TenantlessDbContext> options): base(options) { }


        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);
        //}

        public DbSet<Plan> Plans { get; set; }
        public DbSet<Tenant> Tenants { get; set; }

    }
}
