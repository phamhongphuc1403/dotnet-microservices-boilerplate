using TinyCRM.Identities.Domain.RoleAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identities.Domain.RoleAggregate.DomainServices;

public interface IRoleDomainService
{
    Task<IEnumerable<string>> GetManyAsync(User user);

    Task<Role> CreateAsync(string roleName);

    Task<IEnumerable<Role>> GetAllAsync();
}