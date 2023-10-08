using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace TinyCRM.Identity.Application.Services.Abstractions;

public interface ITokenService
{
    string GenerateAccessToken(IEnumerable<Claim> claims);
    TokenValidationParameters ValidateToken();
}