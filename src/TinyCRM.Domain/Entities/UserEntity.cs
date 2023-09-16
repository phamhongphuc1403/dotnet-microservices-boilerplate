namespace TinyCRM.Domain.Entities;

public class UserEntity : GuidBaseEntity
{
    public string Name { get; set; } = null!;
    public string Email { get; set; } = null!;
    public string RefreshToken { get; set; } = null!;
    public string PasswordHash { get; set; } = null!;
}