using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace LeadForgeCrm.Infrastructure.Seeding
{
    public class PlanSeederHostedService : IHostedService
    {
        private readonly IServiceProvider _serviceProvider;

        public PlanSeederHostedService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _serviceProvider.CreateScope();
            var planSeeder = scope.ServiceProvider.GetRequiredService<PlanSeeder>();
            await planSeeder.SeedAsync();
        }

        public Task StopAsync(CancellationToken cancellationToken) => Task.CompletedTask;
    }
}
