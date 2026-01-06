using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Api.Dtos.Requests;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Entities.SaasCore;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

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
        IPasswordHasher<User> passwordHasher
        )
        : IRequestHandler<SignupCommand, UserDetailsDto>
    {
        public async Task<UserDetailsDto> Handle(SignupCommand request, CancellationToken cancellationToken)
        {
            //create tenant
            var tenant = new Tenant
            {
                PhoneNumber = request.PhoneNumber,
                CreatedAt = DateTime.UtcNow,
                Currency = "INR",
                IsActive = true
            };
            tenant = await tenantRepository.AddAsync(tenant);


            var roles = new[] { "Owner", "Admin", "User" };

            foreach(var role in roles)
            {
                await roleRepository.AddAsync(new Role
                {
                    TenantId = tenant.Id,
                    Name = role,
                    CreatedAt = DateTime.UtcNow
                });
            }

            //get ownere role
            var ownerRole = await roleRepository.GetByNameAsync(
           tenant.Id, "Owner");


            //create first user as ownere
            var user = new User
            {
                TenantId = tenant.Id,
                FullName = request.FullName,
                Email = request.Email,
                RoleId = ownerRole.Id,
                CreatedAt = DateTime.UtcNow
            };
            user.PasswordHash = passwordHasher.HashPassword(user,request.Password);
            await userRepository.AddAsync(user);


            return new UserDetailsDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                TenantId = tenant.Id,
                RoleName = "Owner"
            };
        }
    }


}
