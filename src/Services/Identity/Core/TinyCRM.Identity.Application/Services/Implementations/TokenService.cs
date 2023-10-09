using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace TinyCRM.Identity.Application.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly JwtSetting _jwtSetting;
    private readonly IUserService _userService;

    public TokenService(JwtSetting jwtSetting, IUserService userService)
    {
        _jwtSetting = jwtSetting;
        _userService = userService;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        return GenerateToken(claims, _jwtSetting.AccessTokenExpireTime, _jwtSetting.AccessTokenSecurityKey);
    }

    public async Task<string> GenerateRefreshTokenAsync(IEnumerable<Claim> claims, User user)
    {
        var refreshToken =
            GenerateToken(claims, _jwtSetting.RefreshTokenExpireTime, _jwtSetting.RefreshTokenSecurityKey);

        await _userService.AddRefreshTokenAsync(user, refreshToken);

        return refreshToken;
    }

    public string VerifyRefreshToken(string refreshToken)
    {
        try
        {
            var tokenClaimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(refreshToken,
                ValidateToken(_jwtSetting.RefreshTokenSecurityKey), out _);

            var userId = tokenClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;

            return userId;
        }
        catch (Exception ex)
        {
            throw new AuthenticationException(ex.Message);
        }
    }

    public TokenValidationParameters ValidateToken(string securityKey)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtSetting.Issuer,
            ValidAudience = _jwtSetting.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
        };
    }

    private string GenerateToken(IEnumerable<Claim> claims, int expireMinutes, string securityKey)
    {
        var key = Encoding.UTF8.GetBytes(securityKey);

        var token = new JwtSecurityToken(
            _jwtSetting.Issuer,
            _jwtSetting.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}