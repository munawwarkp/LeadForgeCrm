using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Enums;
using LeadForgeCrm.Domain.Interfaces;

namespace LeadForgeCrm.Infrastructure.Seeding
{
    public class PlanSeeder
    {
        private readonly IplanRepository _planRepository;

        public PlanSeeder(IplanRepository planRepository)
        {
            _planRepository = planRepository;
        }

        public async Task SeedAsync()
        {
            var plansToAdd = new List<Plan>();

            if (!await _planRepository.ExistsByNameAsync("Free Trial"))
            {
                plansToAdd.Add(new Plan
                {
                    Name = "Free Trial",
                    Price = 0,
                    BillingInterval = BillingInterval.Monthly,
                    Currency = "INR",
                    MaxUsers = 2,
                    MaxPipelines = 1,
                    IsTrial = true,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (!await _planRepository.ExistsByNameAsync("Basic"))
            {
                plansToAdd.Add(new Plan
                {
                    Name = "Basic",
                    Price = 299,
                    BillingInterval = BillingInterval.Monthly,
                    Currency = "INR",
                    MaxUsers = 10,
                    MaxPipelines = 2,
                    IsTrial = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }


            if (!await _planRepository.ExistsByNameAsync("Pro"))
            {
                plansToAdd.Add(new Plan
                {
                    Name = "Pro",
                    Price = 999,
                    BillingInterval = BillingInterval.Monthly,
                    Currency = "INR",
                    MaxUsers = 20,
                    MaxPipelines = 5,
                    IsTrial = false,
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                });
            }

            if (plansToAdd.Any())
            {
                await _planRepository.AddRangeAsync(plansToAdd);
            }
        }
    }
}
