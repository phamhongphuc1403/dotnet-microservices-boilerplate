using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Specifications;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.RoleAggregate.DomainServices.Implementations;

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
        var userRoleUserIdSpecification = new UserRoleUserIdSpecification(user.Id);

        var userRoleRoleIdSpecification = new UserRoleRoleIdSpecification(role.Id);

        var specification = userRoleUserIdSpecification.And(userRoleRoleIdSpecification);

        Optional<bool>.Of(await _userRoleReadOnlyRepository.CheckIfExistAsync(specification))
            .ThrowIfExist(new UserRoleConflictException(role.Id, user.Id));
    }

    private async Task CheckValidOnCreate(string roleName)
    {
        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification(roleName);

        Optional<bool>.Of(await _roleReadOnlyRepository.CheckIfExistAsync(roleNameExactMatchSpecification))
            .ThrowIfExist(new RoleConflictException(roleName));
    }
}