using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.Repositories;

public interface IRoleReadOnlyRepository : IReadOnlyRepository<Role>
{
    Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId);
}