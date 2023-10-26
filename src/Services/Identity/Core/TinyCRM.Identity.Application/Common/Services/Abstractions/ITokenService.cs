using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Application.Common.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    TokenValidationParameters ValidateToken(string securityKey);

    string GenerateRefreshToken(IEnumerable<Claim> claims, User user);

    Task<User> VerifyRefreshTokenAsync(string refreshToken);

    Task RevokeAllRefreshTokensAsync(User user);
}