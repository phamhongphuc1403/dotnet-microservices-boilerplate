using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Repositories;

public interface IRoleReadOnlyRepository
{
    Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId);

    Task<List<Role>> GetByUserIdAsync(Guid userId);

    Task<Role?> GetByNameAsync(string roleName, string? includeTables = null);
}