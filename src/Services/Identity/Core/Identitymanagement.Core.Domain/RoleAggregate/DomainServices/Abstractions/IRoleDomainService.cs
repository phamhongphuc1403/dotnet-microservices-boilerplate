using Identitymanagement.Core.Domain.RoleAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;

public interface IRoleDomainService
{
    Task<Role> CreateAsync(string roleName);

    Task AddUserAsync(Role role, User user);
}