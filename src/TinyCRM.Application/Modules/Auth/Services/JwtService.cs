using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Common.Params;
using TinyCRM.Application.Modules.Auth.Services.Interfaces;
using TinyCRM.Domain.Entities;

namespace TinyCRM.Application.Modules.Auth.Services;

public class JwtService : IJwtService
{
    private readonly IIdentityRoleService _identityRoleService;
    private readonly JwtParams _jwtParams;
    private readonly IConfiguration _jwtSettings;

    public JwtService(
        IConfiguration configuration,
        IIdentityRoleService identityRoleService)
    {
        _jwtSettings = configuration.GetSection("JwtSettings");
        _jwtParams = new JwtParams(_jwtSettings);
        _identityRoleService = identityRoleService;
    }

    public async Task<string> GenerateAccessTokenAsync(UserEntity user)
    {
        var roles = await _identityRoleService.GetRoleNamesByUserId(user.Id.ToString());

        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Name, user.Name)
        };

        foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

        var expires = DateTime.UtcNow.AddMinutes(_jwtParams.ExpireMinute);

        return GenerateToken(claims, expires);
    }

    public string GenerateRefreshToken(UserEntity dto)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, dto.Id.ToString())
        };

        var expires = DateTime.UtcNow.AddDays(_jwtParams.ExpireDay);

        return GenerateToken(claims, expires);
    }

    public ClaimsPrincipal Verify(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        return tokenHandler.ValidateToken(token, ValidateToken(_jwtSettings), out _);
    }

    private string GenerateToken(IEnumerable<Claim> claims, DateTime expires)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        var tokenOptions = new JwtSecurityToken(
            _jwtParams.Issuer,
            _jwtParams.Audience,
            claims,
            expires: expires,
            signingCredentials: _jwtParams.SigningCredentials
        );

        var jwt = jwtTokenHandler.WriteToken(tokenOptions);

        return jwt;
    }

    public static TokenValidationParameters ValidateToken(IConfiguration jwtSettings)
    {
        var jwtParams = new JwtParams(jwtSettings);

        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtParams.Issuer,
            ValidAudience = jwtParams.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtParams.SecurityKey))
        };
    }
}