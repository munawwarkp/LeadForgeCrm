
using LeadForgeCrm.Api.Middlewares;
using LeadForgeCrm.Application;
using LeadForgeCrm.Infrastructure;
using LeadForgeCrm.Infrastructure.Seeding;
using Microsoft.OpenApi.Models;

namespace LeadForgeCrmApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);


            //register services from appliction layer
            builder.Services.AddApplicationServices();

            //register services from infrastructure layer
            builder.Services.AddInfrastructureServices(builder.Configuration);

            //builder.Services.AddHttpContextAccessor();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(c =>
               {
                   c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                   {
                       Title = "LeadForgeCrm API",
                       Version = "v1"
                   });

                   //jwt auth definition
                   c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                   {
                       Name = "Authorization",
                       Type = SecuritySchemeType.Http,
                       Scheme = "bearer",
                       BearerFormat = "JWT",
                       In = ParameterLocation.Header,
                       Description = "Enter JWT token like: Bearer {your token}"
                   });

                   c.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                    {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                    }
                            },
                            Array.Empty<string>()
                        }
                    });
               }
            );

            var app = builder.Build();


            // Seed plans at startup
            //using (var scope = app.Services.CreateScope())
            //{
            //    var planSeeder = scope.ServiceProvider.GetRequiredService<PlanSeeder>();
            //    await planSeeder.SeedAsync(); // top-level await
            //}

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseMiddleware<TenantMiddleware>();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
