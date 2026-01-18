using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Entities;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace LeadForgeCrm.Application.Commands
{
    public record SigninCommand (
        string Email,
        string Password)
        : IRequest<Result<SignInResponeDto>>;

    public class SigninCommandHandler(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork
        )
        : IRequestHandler<SigninCommand, Result<SignInResponeDto>>
    {
        public async Task<Result<SignInResponeDto>> Handle(SigninCommand request, CancellationToken cancellationToken)
        {
            //check use exist
            var user = await userRepository.GetEmailAsync(request.Email);
            if(user == null)
            {
                return Result<SignInResponeDto>.Fail("Invalid email or password.");
            }
            
            //validate pw
            var verifyPw = passwordHasher.VerifyHashedPassword(user, user.PasswordHash, request.Password);
            if(verifyPw == PasswordVerificationResult.Failed)
            {
                return Result<SignInResponeDto>.Fail("Invalid email or password.");
            }

            var authContext = new AuthUserContext(
                user.Id,
                user.Email,
                user.Role.Name,
                user.TenantId
                );

            var accessToken = jwtTokenGenerator.GenerateAccessToken(authContext, out DateTime expiresAt);
            var refreshToken = jwtTokenGenerator.GenerateRefreshToken();


            await refreshTokenRepository.AddRefreshTokenAsync(new Domain.Entities.Auth_UserMang.RefreshToken
            {
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                Token = refreshToken,
                UserId = user.Id,
                TenantId = user.TenantId,
            });
          
            await unitOfWork.SaveChangesAsync(cancellationToken);
            //return response
            return Result<SignInResponeDto>.Ok(new SignInResponeDto
            {
                Id = user.Id,
                TenantId = user.TenantId,
                FullName = user.FullName,
                Email = user.Email,
                AccessToken = accessToken,
                RefreshToken = refreshToken,
                ExpiresAt = expiresAt,
            });

            
        }
    }
  
}
