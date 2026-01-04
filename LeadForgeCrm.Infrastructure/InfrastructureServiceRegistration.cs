using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using LeadForgeCrm.Infrastructure.Options;
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

            services.AddScoped<ITenantProvider, TenantProvider>();

            return services;    
        }
    }
}
