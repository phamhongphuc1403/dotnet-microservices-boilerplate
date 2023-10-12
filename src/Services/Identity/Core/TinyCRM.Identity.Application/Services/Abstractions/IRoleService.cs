using TinyCRM.Identities.Domain.RoleAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Application.Services.Abstractions;

public interface IRoleService
{
    Task<IEnumerable<string>> GetManyAsync(User user);

    Task<Role> CreateAsync(string roleName);
}