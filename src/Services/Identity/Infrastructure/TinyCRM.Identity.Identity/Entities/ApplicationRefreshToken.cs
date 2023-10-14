using BuildingBlock.Domain;

namespace TinyCRM.Identity.Identity.Entities;

public class ApplicationRefreshToken : IEntity<Guid>
{
    public ApplicationRefreshToken(Guid userId, string token)
    {
        UserId = userId;
        Token = token;
        CreatedAt = DateTime.UtcNow;
        CreatedBy = "server";
    }

    public Guid UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string Token { get; set; }
    public DateTime? RevokedAt { get; set; }

    public Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }
}