using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Exceptions;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;

namespace IdentityManagement.Core.Domain.PermissionAggregate.DomainServices.Implementations;

public class PermissionDomainService : IPermissionDomainService
{
    private readonly IPermissionReadOnlyRepository _permissionReadOnlyRepository;

    public PermissionDomainService(IPermissionReadOnlyRepository permissionReadOnlyRepository)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
    }

    public async Task CheckValidOnAddRoleAsync(Permission permission, Role role)
    {
        var permissionNames = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(role.Name);

        Optional<bool>.Of(permissionNames.Any(x => x == permission.Name))
            .ThrowIfExist(new RolePermissionConflictException(permission.Name, role.Name));
    }
}