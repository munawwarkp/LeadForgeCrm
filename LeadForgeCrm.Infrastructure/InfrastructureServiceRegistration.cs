using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Application.Services;
using LeadForgeCrm.Domain.Interfaces;
using LeadForgeCrm.Infrastructure.Data;
using LeadForgeCrm.Infrastructure.Options;
using LeadForgeCrm.Infrastructure.Repositories;
using LeadForgeCrm.Infrastructure.Seeding;
using LeadForgeCrm.Infrastructure.ServicesExternal;
using LeadForgeCrm.Infrastructure.Tenancy;
using LeadForgeCrm.Infrastructure.Uow;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

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

            //jwt binding
            services.Configure<JwtSettings>(configuration.GetSection("Jwt"));
            var jwtSettings = configuration
                .GetSection("Jwt")
                .Get<JwtSettings>()
                ?? throw new InvalidOperationException("Jwt settings are missing");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
               .AddJwtBearer(options =>
               {
                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidateIssuer = true,
                       ValidateAudience = true,
                       ValidateLifetime = true,
                       ValidateIssuerSigningKey = true,
                       ValidIssuer = jwtSettings.Issuer,
                       ValidAudience = jwtSettings.Audience,
                       IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Key))
                   };
               });

            services.AddHttpContextAccessor();
            services.AddScoped<ITenantProvider, TenantProvider>();
            services.AddScoped<IUserProvider, UserProvider>();

            services.AddScoped<ITenantRepository, TenantRepository>();
            services.AddScoped<IUserRepository, UserRepository>();  
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IplanRepository, PlanRepository>();
            services.AddScoped<ISubscriptionRepository, SubscriptionRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRespository>();
            services.AddScoped<ILeadRepository, LeadRepository>();
            services.AddScoped<IPipelineRepository, PipelineRepository>();
            services.AddScoped<IPipelineTemplateRepository, PipelineTemplateRepo>();
            services.AddScoped<IPipelineStageRepository, PipelineStageRepository>();
            services.AddScoped<IDealRepository, DealRepository>();
            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
        

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddScoped<IJwtTokenGenerator, JwtTokenGenerator>();

            services.AddScoped<PlanSeeder>();
            services.AddHostedService<PlanSeederHostedService>();
            services.AddScoped<ITenantPipelineInitializer, TenantPipelineInitializer>();

            return services;    
        }
    }
}
