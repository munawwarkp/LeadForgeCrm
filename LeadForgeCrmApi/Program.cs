
using LeadForgeCrm.Application;
using LeadForgeCrm.Infrastructure;
using LeadForgeCrm.Infrastructure.Seeding;

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

            builder.Services.AddHttpContextAccessor();


            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

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

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
