using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeadForgeCrm.Application.Common;
using LeadForgeCrm.Application.Dtos.Responses;
using LeadForgeCrm.Application.Interfaces;
using LeadForgeCrm.Domain.Interfaces;
using MediatR;

namespace LeadForgeCrm.Application.Commands
{
    public record RefreshTokenCommand(string refreshToken) : IRequest<Result<RefreshTokenResult>>;

   public class RefreshTokenCommandHandler(
       IRefreshTokenRepository refreshTokenRepository,
       IUserRepository userRepository,
       IJwtTokenGenerator jwtTokenGenerator,
       IUnitOfWork unitOfWork)
        : IRequestHandler<RefreshTokenCommand, Result<RefreshTokenResult>>
    {
        public async Task<Result<RefreshTokenResult>> Handle(RefreshTokenCommand request, CancellationToken cancellationToken)
        {
            //get refreshtoken
            var storedRefreshToken = await refreshTokenRepository.GetRefreshTokenAsync(request.refreshToken);

            if(storedRefreshToken == null || storedRefreshToken.ExpiresAt < DateTime.UtcNow)
            {
                return Result<RefreshTokenResult>.Fail("Invalid or expired refresh token.");
            }

            if (storedRefreshToken.RevokedAt != null)
                return Result<RefreshTokenResult>.Fail("Refresh token revoked");

            //get user
            var user = await userRepository.GetByIdAsync(storedRefreshToken.UserId);

            if(user == null)
            {
                return Result<RefreshTokenResult>.Fail("User not found.");
            }

            //revoke old token
            storedRefreshToken.RevokedAt = DateTime.UtcNow;
            storedRefreshToken.Revoked = true;
            refreshTokenRepository.Update(storedRefreshToken);

            //generate new tokens
            var accessToken = jwtTokenGenerator.GenerateAccessToken(
                new AuthUserContext(
                    user.Id,
                    user.Email,
                    user.Role.Name,
                    user.TenantId
                    ),
                out DateTime expiresAt);

            var refreshToken = jwtTokenGenerator.GenerateRefreshToken();

            await refreshTokenRepository.AddRefreshTokenAsync(new Domain.Entities.Auth_UserMang.RefreshToken
            {
                TokenHash = refreshToken,
                UserId = user.Id,
                TenantId = user.TenantId,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = DateTime.UtcNow.AddDays(7),
                Revoked = false
            });

            await unitOfWork.SaveChangesAsync(cancellationToken);

            return Result<RefreshTokenResult>.Ok(new RefreshTokenResult
            {               
                AccessToken = accessToken,
                RefreshToken = refreshToken
            });
        }
    }
}
