using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.DomainServices;

public interface IRoleDomainService
{
    Task<IEnumerable<string>> GetManyAsync(User user);

    Task<Role> CreateAsync(string roleName);

    Task<IEnumerable<Role>> GetAllAsync();
}