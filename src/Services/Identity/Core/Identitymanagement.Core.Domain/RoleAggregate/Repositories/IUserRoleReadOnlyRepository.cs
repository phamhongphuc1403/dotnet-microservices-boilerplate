using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.Repositories;

public interface IUserRoleReadOnlyRepository
{
    Task<UserRole?> GetByUserIdAndRoleIdAsync(Guid userId, Guid roleId);
}