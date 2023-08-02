using System.Security.Claims;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Auth.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(UserEntity user);

        string GenerateRefreshToken(UserEntity user);

        ClaimsPrincipal? Verify(string token);
    }
}