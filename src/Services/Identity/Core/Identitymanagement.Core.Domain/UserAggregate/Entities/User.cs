using BuildingBlock.Core.Domain;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.UserAggregate.Entities;

public class User : AggregateRoot
{
    public User(string email)
    {
        Email = email;
    }

    public User()
    {
    }

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string SecurityStamp { get; set; } = null!;

    public string ConcurrencyStamp { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public bool TwoFactorEnabled { get; set; }

    public bool LockoutEnabled { get; set; } = true;

    public int AccessFailedCount { get; set; }

    public ICollection<RefreshToken> RefreshTokens { get; set; } = null!;

    public ICollection<UserRole> UserRoles { get; set; } = null!;
}