using BuildingBlock.Core.Domain.Shared.Utils;
using Identitymanagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;
using Identitymanagement.Core.Domain.RoleAggregate.Exceptions;
using Identitymanagement.Core.Domain.RoleAggregate.Repositories;
using Identitymanagement.Core.Domain.UserAggregate.Entities;

namespace Identitymanagement.Core.Domain.RoleAggregate.DomainServices.Implementations;

public class RoleDomainService : IRoleDomainService
{
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUserRoleReadOnlyRepository _userRoleReadOnlyRepository;

    public RoleDomainService(IRoleReadOnlyRepository roleReadOnlyRepository,
        IUserRoleReadOnlyRepository userRoleReadOnlyRepository)
    {
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _userRoleReadOnlyRepository = userRoleReadOnlyRepository;
    }

    public async Task<Role> CreateAsync(string roleName)
    {
        await CheckValidOnCreate(roleName);

        return new Role(roleName);
    }

    public async Task AddUserAsync(Role role, User user)
    {
        await CheckValidOnAddUser(role, user);

        role.UserRoles.Add(new UserRole(user.Id));
    }

    private async Task CheckValidOnAddUser(Role role, User user)
    {
        Optional<UserRole>.Of(await _userRoleReadOnlyRepository.GetByUserIdAndRoleIdAsync(role.Id, user.Id))
            .ThrowIfExist(new UserRoleConflictException(role.Id, user.Id));
    }

    private async Task CheckValidOnCreate(string roleName)
    {
        Optional<Role>.Of(await _roleReadOnlyRepository.GetByNameAsync(roleName))
            .ThrowIfExist(new RoleConflictException(roleName));
    }
}