using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TinyCRM.Identities.Application.Services.Interfaces;

public interface ITokenService
{
    string GenerateTokens(IEnumerable<Claim> claims, int expireMinutes);
    TokenValidationParameters ValidateToken();
}