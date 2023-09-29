using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TinyCRM.Identity.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateTokens(IEnumerable<Claim> claims, int expireMinutes);
    TokenValidationParameters ValidateToken();
}