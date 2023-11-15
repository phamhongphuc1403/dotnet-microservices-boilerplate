using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Repositories;

public interface IUserRoleReadOnlyRepository
{
    Task<UserRole?> GetByUserIdAndRoleIdAsync(Guid userId, Guid roleId);
}