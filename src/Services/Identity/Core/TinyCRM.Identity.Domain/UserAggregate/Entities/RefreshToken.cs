using BuildingBlock.Domain;

namespace TinyCRM.Identity.Domain.UserAggregate.Entities;

public class RefreshToken : Entity
{
    public RefreshToken(string token)
    {
        Token = token;
    }

    public RefreshToken()
    {
    }

    public Guid UserId { get; set; }

    public string Token { get; set; } = null!;

    public User User { get; set; } = null!;

    public DateTime? RevokedAt { get; set; }

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }
}