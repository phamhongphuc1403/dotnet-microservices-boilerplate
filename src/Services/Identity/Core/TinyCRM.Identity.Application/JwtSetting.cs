namespace TinyCRM.Identity.Application;

public class JwtSetting
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string AccessTokenSecurityKey { get; set; } = null!;
    public int AccessTokenExpireTime { get; set; }
    public string RefreshTokenSecurityKey { get; set; } = null!;
    public int RefreshTokenExpireTime { get; set; }
}