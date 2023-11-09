using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.Repositories;

public interface IRoleReadOnlyRepository
{
    Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId);

    Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId);

    Task<Role?> GetByNameAsync(string roleName, string? includeTables = null);
}