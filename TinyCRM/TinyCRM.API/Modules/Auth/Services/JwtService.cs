using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.Params;

namespace TinyCRM.API.Modules.Auth.Services
{
    public class JwtService : IJwtService
    {
        private readonly JwtParams _jwtParams;
        private readonly IConfiguration _jwtSettings;
        private readonly UserManager<UserEntity> _userManager;

        public JwtService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _jwtSettings = configuration.GetSection("JwtSettings");
            _jwtParams = new JwtParams(_jwtSettings);
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessTokenAsync(UserEntity user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault() ?? throw new InvalidOperationException("User has no role");

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.Name ?? ""),
                new(ClaimTypes.Role, role)
            };

            var expires = DateTime.Now.AddMinutes(_jwtParams.ExpireMinute);

            return GenerateToken(claims, expires);
        }

        private string GenerateToken(List<Claim> claims, DateTime expires)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenOptions = new JwtSecurityToken(
                issuer: _jwtParams.Issuer,
                audience: _jwtParams.Audience,
                claims: claims,
                expires: expires,
                signingCredentials: _jwtParams.SigningCredentials
            );

            var jwt = jwtTokenHandler.WriteToken(tokenOptions);

            return jwt;
        }

        public string GenerateRefreshToken(UserEntity user)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
            };

            var expires = DateTime.Now.AddDays(_jwtParams.ExpireDay);

            return GenerateToken(claims, expires);
        }

        public ClaimsPrincipal? Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.ValidateToken(token, ValidateToken(_jwtSettings), out var validatedToken);
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

}