using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Entities.SaasCore;
using Microsoft.EntityFrameworkCore;

namespace LeadForgeCrm.Infrastructure.Data
{
    public class AppDbContext:DbContext
    {
        private readonly ITenantProvider _tenantProvider;
        public AppDbContext(DbContextOptions<AppDbContext> options, ITenantProvider tenantProvider):base(options)
        {
            _tenantProvider = tenantProvider;
        }

        public int CurrentTenantId => _tenantProvider.TenantId;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //global tenant query filters
            modelBuilder.Entity<User>()
                .HasQueryFilter(u => u.TenantId == CurrentTenantId);

            modelBuilder.Entity<Contact>()
                .HasQueryFilter(c => c.TenantId == CurrentTenantId);

            modelBuilder.Entity<Lead>()
                .HasQueryFilter(l => l.TenantId == CurrentTenantId);

            modelBuilder.Entity<Deal>()
                .HasQueryFilter(d => d.TenantId == CurrentTenantId);

            modelBuilder.Entity<Activity>()
                .HasQueryFilter(a => a.TenantId == CurrentTenantId);

            modelBuilder.Entity<CrmTask>()
                .HasQueryFilter(t => t.TenantId == CurrentTenantId);
            
            modelBuilder.Entity<PipeLine>()
                .HasQueryFilter(p => p.TenantId == CurrentTenantId);

            modelBuilder.Entity<PipelineStage>()
                .HasQueryFilter(ps => ps.TenantId == CurrentTenantId);


            //unique rule
            modelBuilder.Entity<User>()
                .HasIndex(u => new { u.TenantId, u.Email })
                .IsUnique();

            // Optional indexes (recommended)
            modelBuilder.Entity<Lead>()
                .HasIndex(l => l.TenantId);

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.TenantId);
        }
      
        //dbsets (tables)

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Plan> Plans { get; set; }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CrmTask> CrmTasks { get; set; }


        public DbSet<PipeLine> PipeLines { get; set; }
        public DbSet<PipelineStage> PipelineStages { get; set; }

    }
}
