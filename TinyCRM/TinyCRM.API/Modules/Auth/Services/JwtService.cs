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
        private readonly SymmetricSecurityKey _secretKeyBytes;
        private readonly int _expireMinute;
        private readonly int _expireDay;
        private readonly string _issuer;
        private readonly string _audience;
        private readonly UserManager<UserEntity> _userManager;

        public JwtService(IConfiguration configuration, UserManager<UserEntity> userManager)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

            _secretKeyBytes = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]));

            _signingCredentials = new SigningCredentials(_secretKeyBytes, SecurityAlgorithms.HmacSha256);
            _expireMinute = int.Parse(jwtSettings["expireMinute"]);
            _expireDay = int.Parse(jwtSettings["expireDay"]);
            _userManager = userManager;
            _issuer = jwtSettings["validIssuer"];
            _audience = jwtSettings["validAudience"];
        }

        public async Task<string> GenerateAccessTokenAsync(UserEntity user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var roles = await _userManager.GetRolesAsync(user);
            var role = roles.FirstOrDefault();

            var tokenOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new(ClaimTypes.Name, user.Name ?? ""),
                    new(ClaimTypes.Role, role)
                },
                expires: DateTime.Now.AddMinutes(_expireMinute),
                signingCredentials: _signingCredentials
            );

            var jwt = jwtTokenHandler.WriteToken(tokenOptions);

            return jwt;
        }

        public string GenerateRefreshToken(UserEntity user)
        {
            var jwtTokenHandler = new JwtSecurityTokenHandler();

            var tokenOptions = new JwtSecurityToken(
                issuer: _issuer,
                audience: _audience,
                claims: new List<Claim>
                {
                    new(ClaimTypes.NameIdentifier, user.Id),
                },
                expires: DateTime.Now.AddDays(_expireDay),
                signingCredentials: _signingCredentials
            );

            var jwt = jwtTokenHandler.WriteToken(tokenOptions);

            return jwt;
        }

        public ClaimsPrincipal? Verify(string token)
        {
            var tokenHandler = new JwtSecurityTokenHandler();

            try
            {
                return tokenHandler.ValidateToken(token, ValidateToken(), out var validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

        public TokenValidationParameters ValidateToken()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = _issuer,
                ValidAudience = _audience,
                IssuerSigningKey = _secretKeyBytes
            };
        }
    }
}