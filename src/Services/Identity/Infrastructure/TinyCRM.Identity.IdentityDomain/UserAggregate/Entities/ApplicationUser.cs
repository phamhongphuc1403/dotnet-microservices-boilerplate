using BuildingBlock.Domain;
using Microsoft.AspNetCore.Identity;

namespace TinyCRM.Identity.Identity.UserAggregate.Entities;

public sealed class ApplicationUser : IdentityUser<Guid>, IAggregateRoot
{
    public ApplicationUser(string email, string? createdBy) : this(email)
    {
        if (createdBy != null) CreatedBy = createdBy;
    }

    public ApplicationUser(string email) : this()
    {
        UserName = email;
        Email = email;
    }

    public ApplicationUser()
    {
        CreatedAt = DateTime.UtcNow;
        CreatedBy = "server";
    }

    public ICollection<ApplicationRefreshToken> RefreshTokens { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}