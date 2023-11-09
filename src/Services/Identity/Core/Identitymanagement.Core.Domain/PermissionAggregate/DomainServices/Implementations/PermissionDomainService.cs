using BuildingBlock.Core.Domain.Shared.Utils;
using Identitymanagement.Core.Domain.PermissionAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.PermissionAggregate.Entities;
using Identitymanagement.Core.Domain.PermissionAggregate.Exceptions;
using Identitymanagement.Core.Domain.PermissionAggregate.Repositories;
using Identitymanagement.Core.Domain.RoleAggregate.Entities;

namespace Identitymanagement.Core.Domain.PermissionAggregate.DomainServices.Implementations;

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