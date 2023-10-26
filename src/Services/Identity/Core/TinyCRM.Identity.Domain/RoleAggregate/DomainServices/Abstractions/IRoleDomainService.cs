using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.RoleAggregate.DomainServices.Abstractions;

public interface IRoleDomainService
{
    Task<Role> CreateAsync(string roleName);

    Task AddUserAsync(Role role, User user);
}