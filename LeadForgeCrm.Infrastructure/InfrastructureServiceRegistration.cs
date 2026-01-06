using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using LeadForgeCrm.Infrastructure.Options;
using LeadForgeCrm.Infrastructure.Repositories;
using LeadForgeCrm.Infrastructure.Seeding;
using LeadForgeCrm.Infrastructure.Tenancy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace LeadForgeCrm.Infrastructure
{
    public static class InfrastructureServiceRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConnectionStringOptions>(configuration.GetSection(ConnectionStringOptions.SectionName));

            services.AddDbContext<AppDbContext>((provider, options) =>
            {
                var connOptions = provider.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;

                options.UseSqlServer(connOptions.DefaultConnection);
            });

            services.AddDbContext<TenantlessDbContext>((provider, options) =>
            {
                var connOptions = provider.GetRequiredService<IOptions<ConnectionStringOptions>>().Value;
                if (string.IsNullOrWhiteSpace(connOptions.DefaultConnection))
                    throw new InvalidOperationException("DefaultConnection string is not configured.");

                options.UseSqlServer(connOptions.DefaultConnection);
            });

            services.AddScoped<ITenantProvider, TenantProvider>();

            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IplanRepository, PlanRepository>();


            services.AddScoped<PlanSeeder>();
            services.AddHostedService<PlanSeederHostedService>();

            return services;    
        }
    }
}
