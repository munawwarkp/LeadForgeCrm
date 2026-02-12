using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Entities.Auth_UserMang;
using LeadForgeCrm.Domain.Entities.Base;
using LeadForgeCrm.Domain.Entities.CrmCore;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Entities.SysData;
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

        public int? CurrentTenantId => _tenantProvider.TenantId;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //global tenant query filters
            modelBuilder.Entity<User>()
                .HasQueryFilter(u =>
                    !CurrentTenantId.HasValue || u.TenantId == CurrentTenantId);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(r =>
                    !CurrentTenantId.HasValue || r.TenantId == CurrentTenantId);

            modelBuilder.Entity<Contact>()
                .HasQueryFilter(c =>
                   !CurrentTenantId.HasValue || c.TenantId == CurrentTenantId);

            modelBuilder.Entity<Lead>()
                .HasQueryFilter(l => 
                    !CurrentTenantId.HasValue || l.TenantId == CurrentTenantId);

            modelBuilder.Entity<Deal>()
                .HasQueryFilter(d =>
                     !CurrentTenantId.HasValue || d.TenantId == CurrentTenantId);

            modelBuilder.Entity<Activity>()
                .HasQueryFilter(a =>
                    !CurrentTenantId.HasValue || a.TenantId == CurrentTenantId);

            modelBuilder.Entity<CrmTask>()
                .HasQueryFilter(t =>
                    !CurrentTenantId.HasValue || t.TenantId == CurrentTenantId);
            
            modelBuilder.Entity<PipeLine>()
                .HasQueryFilter(p => 
                    !CurrentTenantId.HasValue || p.TenantId == CurrentTenantId);

            modelBuilder.Entity<PipelineStage>()
                .HasQueryFilter(ps =>
                    !CurrentTenantId.HasValue || ps.TenantId == CurrentTenantId);


            modelBuilder.Entity<RefreshToken>()
                .HasQueryFilter(rt =>
                    !CurrentTenantId.HasValue || rt.TenantId == CurrentTenantId);

            modelBuilder.Entity<PipelineStage>()
                .HasQueryFilter(ps =>
                   !CurrentTenantId.HasValue || ps.TenantId == CurrentTenantId);

            modelBuilder.Entity<PipeLine>()
                .HasQueryFilter(p =>
                    !CurrentTenantId.HasValue || p.TenantId == CurrentTenantId);

            //tenant configuration

            modelBuilder.Entity<Tenant>()
                .HasOne(t => t.Subscription)
                .WithOne(s => s.Tenant)
                .HasForeignKey<Subscription>(s => s.TenantId)
                .OnDelete(DeleteBehavior.Cascade);

            //subscription configuration

            modelBuilder.Entity<Subscription>(entity =>
            {
                // One subscription per tenant
                entity.HasIndex(s => s.TenantId)
                      .IsUnique();

                // Subscription → Plan (many : 1)
                entity.HasOne(s => s.Plan)
                .WithMany()
                .HasForeignKey(s => s.PlanId)
                .OnDelete(DeleteBehavior.Restrict);
            });

            //plan configuration
            modelBuilder.Entity<Plan>(entity =>
            {
                entity.Property(p => p.Price)
                      .HasPrecision(18, 2)
                      .IsRequired();

                entity.HasIndex(p => p.Name)
                      .IsUnique();
            });


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
            });

            modelBuilder.Entity<Company>(entity =>
            {
                entity.HasOne(c => c.Tenant)
                      .WithMany()
                      .HasForeignKey(c => c.TenantId)
                      .OnDelete(DeleteBehavior.Restrict);   // <-- important
            });

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
                      .OnDelete(DeleteBehavior.SetNull);

                entity.HasOne(c => c.Tenant)
                     .WithMany()
                     .HasForeignKey(c => c.TenantId)
                     .OnDelete(DeleteBehavior.Restrict);

                // Index for multi-tenancy
                entity.HasIndex(c => c.TenantId);

                entity.HasIndex(c => new { c.TenantId, c.Email })
                   .IsUnique()
                   .HasFilter("[Email] IS NOT NULL");

            });


            modelBuilder.Entity<Lead>(entity =>
            {
                // AssignedTo relationship
                entity.HasOne(l => l.AssignedTo)
                      .WithMany() // or .WithMany(u => u.Leads) if User has a Leads collection
                      .HasForeignKey(l => l.AssignedToId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Index for multi-tenancy
                entity.HasIndex(l => new { l.TenantId, l.CreatedAt });
            });

            modelBuilder.Entity<Deal>(entity =>
            {
                entity.Property(d => d.Amount)
                      .HasPrecision(18,2)
                      .IsRequired();

                entity.HasOne(d => d.Company)
                    .WithMany(c => c.Deals)
                    .HasForeignKey(d => d.CompanyId)
                    .OnDelete(DeleteBehavior.Restrict); 

                entity.HasOne(d => d.Contact)
                    .WithMany(c => c.Deals)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            modelBuilder.Entity<Activity>(entity =>
            {
                entity.HasOne(a => a.Lead)
                    .WithMany(l => l.Activities) // Lead has ICollection<Activity>
                    .HasForeignKey(a => a.LeadId)
                    .OnDelete(DeleteBehavior.Cascade);

                // User relationship
                entity.HasOne(a => a.User)
                      .WithMany(u => u.Activities) // ← specify the navigation collection
                      .HasForeignKey(a => a.UserId)
                      .OnDelete(DeleteBehavior.Restrict);

                // Tenant index for multi-tenancy
                entity.HasIndex(a => a.TenantId);
            });

            modelBuilder.Entity<RefreshToken>(entity =>
            {
                entity.HasIndex(rt => rt.Token)
                       .IsUnique();

                entity.HasOne(x => x.User)
                     .WithMany()
                     .HasForeignKey(x => x.UserId)
                     .OnDelete(DeleteBehavior.Cascade);

                entity.HasOne(x => x.Tenant)
                   .WithMany()
                   .HasForeignKey(x => x.TenantId)
                   .OnDelete(DeleteBehavior.Restrict);

            });

            modelBuilder.Entity<CrmTask>(entity =>
            {
                entity.HasOne(t => t.Tenant)
                        .WithMany()
                        .HasForeignKey(t => t.TenantId)
                        .OnDelete(DeleteBehavior.Restrict);
            });
        }

        //dbsets (tables)

        public DbSet<Tenant> Tenants { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }


        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }


        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Lead> Leads { get; set; }
        public DbSet<Deal> Deals { get; set; }
        public DbSet<Activity> Activities { get; set; }
        public DbSet<CrmTask> CrmTasks { get; set; }
        public DbSet<Company> Companies { get; set; }



        public DbSet<PipeLine> PipeLines { get; set; }
        public DbSet<PipelineStage> PipelineStages { get; set; }


        public DbSet<PipelineTemplates> PipelineTemplates { get; set; }
        public DbSet<PipelineStageTemplate> PipelineStageTemplates { get; set; }

    }
}
