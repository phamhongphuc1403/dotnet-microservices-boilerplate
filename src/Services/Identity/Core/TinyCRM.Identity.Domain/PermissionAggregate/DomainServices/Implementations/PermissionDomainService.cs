using BuildingBlock.Domain.Shared.Utils;
using TinyCRM.Identity.Domain.PermissionAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.PermissionAggregate.Exceptions;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;

namespace TinyCRM.Identity.Domain.PermissionAggregate.DomainServices.Implementations;

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
            .ThrowIfPresent(new RolePermissionConflictException(permission.Name, role.Name));
    }
}