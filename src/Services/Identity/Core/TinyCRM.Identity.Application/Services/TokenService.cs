using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TinyCRM.Identity.Application.Services.Interfaces;

namespace TinyCRM.Identity.Application.Services;

public class TokenService : ITokenService
{
    private readonly JwtSetting _jwtSetting;

    public TokenService(JwtSetting jwtSetting)
    {
        _jwtSetting = jwtSetting;
    }

    public string GenerateTokens(IEnumerable<Claim> claims, int expireMinutes)
    {
        var key = Encoding.UTF8.GetBytes(_jwtSetting.Key);

        var token = new JwtSecurityToken(
            _jwtSetting.Issuer,
            _jwtSetting.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
    
    public TokenValidationParameters ValidateToken()
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSetting.Issuer,
            ValidAudience = _jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSetting.Key))
        };
    }
}