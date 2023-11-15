using BuildingBlock.Core.Domain;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Entities;

public sealed class UserRole : Entity
{
    public UserRole(Guid userId)
    {
        UserId = userId;
    }

    public UserRole()
    {
    }

    public Guid UserId { get; set; }

    public User User { get; set; } = null!;

    public Guid RoleId { get; set; }

    public Role Role { get; set; } = null!;
}