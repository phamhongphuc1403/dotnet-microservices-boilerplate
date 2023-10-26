using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.Repositories;

public interface IRoleReadOnlyRepository
{
    Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId);

    Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId);

    Task<Role?> GetByNameAsync(string roleName, string? includeTables = null);
}