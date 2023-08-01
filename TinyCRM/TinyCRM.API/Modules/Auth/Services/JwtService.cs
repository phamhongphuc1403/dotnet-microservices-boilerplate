using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Modules.Auth.Services
{
    public class JwtService : IJwtService
    {
        private readonly SigningCredentials _signingCredentials;
        private readonly int _expireMinute;
        private readonly int _expireDay;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly IConfiguration _jwtSettings;
        private readonly UserManager<UserEntity> _userManager;

        public JwtService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            _jwtSettings = configuration.GetSection("JwtSettings");
            _signingCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings["securityKey"])), SecurityAlgorithms.HmacSha256);
            _expireMinute = int.Parse(_jwtSettings["expireMinute"]);
            _expireDay = int.Parse(_jwtSettings["expireDay"]);
            _issuer = _jwtSettings["validIssuer"];
            _audience = _jwtSettings["validAudience"];
            _userManager = userManager;
        }

        public async Task<string> GenerateAccessTokenAsync(UserEntity user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var claims = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, user.Id),
                new(ClaimTypes.Name, user.Name ?? ""),
                new(ClaimTypes.Role, role)
            };

            var expires = DateTime.Now.AddMinutes(_expireMinute);

            return GenerateToken(claims, expires);
        }

        private string GenerateToken(List<Claim> claims, DateTime expires)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: claims,
                expires: expires,
                signingCredentials: _signingCredentials
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

            var expires = DateTime.Now.AddDays(_expireDay);

            return GenerateToken(claims, expires);
        }

        public ClaimsPrincipal? Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            return tokenHandler.ValidateToken(token, ValidateToken(_jwtSettings), out var validatedToken);
        }

        public static TokenValidationParameters ValidateToken(IConfiguration jwtSettings)
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtSettings["validIssuer"],
                ValidAudience = jwtSettings["validAudience"],
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
            };
        }
    }
}