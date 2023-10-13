namespace TinyCRM.Identity.Identity.Entities;

public class ApplicationRefreshToken
{
    public ApplicationRefreshToken(string userId, string token)
    {
        UserId = userId;
        Token = token;
        CreatedAt = DateTime.UtcNow;
    }

    public Guid Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = null!;
    public string Token { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime? RevokedAt { get; set; }

    public void Revoke()
    {
        RevokedAt = DateTime.UtcNow;
    }
}