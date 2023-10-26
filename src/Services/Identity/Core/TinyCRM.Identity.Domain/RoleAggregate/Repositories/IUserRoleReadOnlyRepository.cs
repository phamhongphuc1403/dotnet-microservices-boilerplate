using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.Repositories;

public interface IUserRoleReadOnlyRepository
{
    Task<UserRole?> GetByUserIdAndRoleIdAsync(Guid userId, Guid roleId);
}