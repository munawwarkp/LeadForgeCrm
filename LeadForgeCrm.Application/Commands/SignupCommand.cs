using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Api.Dtos.Requests;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace LeadForgeCrm.Application.Commands
{
    public record SignupCommand(
            string FullName,
            string Email,
            string Password,
            string CompanyName,
            string PhoneNumber
        ) :IRequest<UserDetailsDto>;


    public class SignupCommandHandler(
        ITenantRepository tenantRepository,
        IUserRepository userRepository,
        IRoleRepository roleRepository,
        IPasswordHasher<User> passwordHasher,
        IplanRepository planRepository,
        ISubscriptionRepository subscriptionRepository,
        IUnitOfWork unitOfWork,
        ILogger<SignupCommandHandler> logger
        )
        : IRequestHandler<SignupCommand, UserDetailsDto>
    {
        public async Task<UserDetailsDto> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            //await unitOfWork.BeginTransactionAsync(cancellationToken);

            try
            {
                //create tenant
                var tenant = new Tenant
                {
                    PhoneNumber = request.PhoneNumber,
                    CreatedAt = DateTime.UtcNow,
                    Currency = "INR",
                    IsActive = true
                };

                tenant = tenantRepository.Add(tenant);

                //create subscription
                var defaultPlan = await planRepository.GetDefaultFreePlanAsync();

                var subscription = new Subscription
                {
                    Tenant = tenant,
                    PlanId = defaultPlan.Id,
                    EndDate = DateTime.UtcNow.AddDays(14), //trial
                    IsActive = true,
                    CreatedAt = DateTime.UtcNow
                };


                 subscriptionRepository.AddSubscription(subscription);


                //seed roles and keep referrnces
                var rolesToAdd = new List<Role>();
                var roles = new[] { "Owner", "Admin", "User" };

                foreach (var role in roles)
                {
                    var roleEntity = new Role
                    {
                        Tenant = tenant,
                        Name = role,
                        CreatedAt = DateTime.UtcNow
                    };
                    roleRepository.Add(roleEntity);
                    rolesToAdd.Add(roleEntity);
                }


                ////get ownere role
                //var ownerRole = await roleRepository.GetByNameAsync(tenant.Id, roles[0]);
                //if (ownerRole == null)
                //{
                //    throw new InvalidOperationException("Owner role not found for this tenant. Make sure roles are seeded.");

                //}

                //get the in memory owner role (not from db)
                var ownerRole = rolesToAdd.First(r => r.Name == "Owner");


                //create first user with the in-memory ownerRole.Id (  will be set after SaveChanges)
                var user = new User
                {
                    Tenant = tenant,
                    FullName = request.FullName,
                    Email = request.Email,
                    Role = ownerRole,
                    CreatedAt = DateTime.UtcNow
                };

                user.PasswordHash = passwordHasher.HashPassword(user, request.Password);
                userRepository.Add(user);

                await unitOfWork.SaveChangesAsync(cancellationToken);

                return new UserDetailsDto
                {
                    Id = user.Id,
                    FullName = user.FullName,
                    Email = user.Email,
                    TenantId = tenant.Id,
                    RoleName = "Owner"
                };
            }
            catch(Exception ex)
            {
                logger.LogError(ex, "Error during signup process");
                throw;
            }
           
        }
    }


}
