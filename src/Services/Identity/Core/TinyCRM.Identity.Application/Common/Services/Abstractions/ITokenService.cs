using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Application.Common.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    TokenValidationParameters ValidateToken(string securityKey);
    Task<string> GenerateRefreshTokenAsync(IEnumerable<Claim> claims, User user);
    Guid VerifyRefreshToken(string refreshToken);
}