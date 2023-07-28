using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Auth.Services
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(UserEntity user);
        string GenerateRefreshToken(UserEntity user);
        ClaimsPrincipal? Verify(string token);
        TokenValidationParameters ValidateToken();
    }
}
