using System.Security.Claims;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using Microsoft.IdentityModel.Tokens;

namespace IdentityManagement.Core.Application.Common.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);

    TokenValidationParameters ValidateToken(string securityKey);

    string GenerateRefreshToken(IEnumerable<Claim> claims, User user);

    Task<User> VerifyRefreshTokenAsync(string refreshToken);

    Task RevokeAllRefreshTokensAsync(User user);
}