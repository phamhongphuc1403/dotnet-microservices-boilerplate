using System.Security.Claims;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Auth.Services.Interfaces
{
    public interface IJwtService
    {
        Task<string> GenerateAccessTokenAsync(UserEntity user);

        string GenerateRefreshToken(UserEntity dto);

        ClaimsPrincipal Verify(string token);
    }
}