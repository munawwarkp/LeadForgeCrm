using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
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


        // Design-time constructor for EF Core tools
        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
            _tenantProvider = null; // no tenant at design time
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

            modelBuilder.Entity<Company>()
                .HasQueryFilter(c => c.TenantId == CurrentTenantId);

            modelBuilder.Entity<Subscription>()
                .HasQueryFilter(s => s.TenantId == CurrentTenantId);


            modelBuilder.Entity<User>(entity =>
            {
                // 1️⃣ Enforce unique email per tenant (important for SaaS)
                entity.HasIndex(u => new { u.TenantId, u.Email })
                      .IsUnique();

                // 2️⃣ Configure Role relationship and delete behavior
                entity.HasOne(u => u.Role)
                      .WithMany() // or .WithMany(r => r.Users) if Role has Users collection
                      .HasForeignKey(u => u.RoleId)
                      .OnDelete(DeleteBehavior.Restrict); // prevent cascading delete of users if role is deleted

                // 3️⃣ Configure Tenant relationship and delete behavior
                entity.HasOne(u => u.Tenant)
                      .WithMany(t => t.Users)
                      .HasForeignKey(u => u.TenantId)
                      .OnDelete(DeleteBehavior.Restrict); // prevent cascading delete of users if tenant is deleted
            });


            // Optional indexes (recommended)
            modelBuilder.Entity<Lead>()
                .HasIndex(l => new { l.TenantId ,l.CreatedAt});

            modelBuilder.Entity<Contact>()
                .HasIndex(c => c.TenantId);






            modelBuilder.Entity<Contact>(entity =>
            {
                // Owner relationship
                entity.HasOne(c => c.Owner)
                      .WithMany()            // or .WithMany(u => u.Contacts) if User has collection
                      .HasForeignKey(c => c.OwnerId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Company relationship
                entity.HasOne(c => c.Company)
                      .WithMany(comp => comp.Contacts)
                      .HasForeignKey(c => c.CompanyId)
                      .OnDelete(DeleteBehavior.Cascade);

                // Index for multi-tenancy
                entity.HasIndex(c => c.TenantId);
            });


            modelBuilder.Entity<Lead>(entity =>
            {
                // Contact relationship
                entity.HasOne(l => l.Contact)
                      .WithMany(c => c.Leads)
                      .HasForeignKey(l => l.ContactId)
                      .OnDelete(DeleteBehavior.Cascade);

                // AssignedTo relationship
                entity.HasOne(l => l.AssignedTo)
                      .WithMany() // or .WithMany(u => u.Leads) if User has a Leads collection
                      .HasForeignKey(l => l.AssignedToId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Index for multi-tenancy
                entity.HasIndex(l => l.TenantId);
            });


            modelBuilder.Entity<Activity>(entity =>
            {
                // Lead relationship
                entity.HasOne(a => a.Lead)
                      .WithMany(l => l.Activities) // Lead should have ICollection<Activity> Activities
                      .HasForeignKey(a => a.LeadId)
                      .OnDelete(DeleteBehavior.Cascade);

                // User relationship
                entity.HasOne(a => a.User)
                      .WithMany() // or .WithMany(u => u.Activities) if User has ICollection<Activity>
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Tenant index for multi-tenancy
                entity.HasIndex(a => a.TenantId);
            });


        }

        //dbsets (tables)

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CrmTask> CrmTasks { get; set; }
        public DbSet<Company> Companies { get; set; }



        public DbSet<PipeLine> PipeLines { get; set; }
        public DbSet<PipelineStage> PipelineStages { get; set; }

    }
}
