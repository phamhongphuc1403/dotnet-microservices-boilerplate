using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;

public interface IRoleDomainService
{
    Task<Role> CreateAsync(string roleName);

    Task AddUserAsync(Role role, User user);
}