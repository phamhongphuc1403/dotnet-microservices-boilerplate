namespace TinyCRM.Identity.Application;

public class JwtSetting
{
    public string Audience { get; set; } = null!;
    public string Issuer { get; set; } = null!;
    public string Key { get; set; } = null!;
}