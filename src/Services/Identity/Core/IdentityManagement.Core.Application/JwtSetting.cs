namespace IdentityManagement.Core.Application;

public class JwtSetting
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string AccessTokenSecurityKey { get; set; } = null!;
    public int AccessTokenLifeTimeInMinute { get; set; }
    public string RefreshTokenSecurityKey { get; set; } = null!;
    public int RefreshTokenLifeTimeInMinute { get; set; }
}