using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace TinyCRM.Infrastructure.JWT
{
    public class JwtParams
    {
        public SigningCredentials SigningCredentials { get; set; }
        public int ExpireMinute { get; set; }
        public int ExpireDay { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string SecurityKey { get; set; }

        public JwtParams(IConfiguration jwtSettings)
        {
            var securityKey = jwtSettings["securityKey"];
            var expireMinute = jwtSettings["expireMinute"];
            var expireDay = jwtSettings["expireDay"];
            var issuer = jwtSettings["validIssuer"];
            var audience = jwtSettings["validAudience"];

            SecurityKey = securityKey ?? throw new InvalidOperationException(nameof(securityKey));

            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(SecurityKey)),
                SecurityAlgorithms.HmacSha256);

            ExpireMinute = expireMinute != null
                ? int.Parse(expireMinute)
                : throw new InvalidOperationException(nameof(expireMinute));

            ExpireDay = expireDay != null
                ? int.Parse(expireDay)
                : throw new InvalidOperationException(nameof(expireDay));

            Issuer = issuer ?? throw new InvalidOperationException(nameof(issuer));

            Audience = audience ?? throw new InvalidOperationException(nameof(audience));
        }
    }
}