using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.PermissionAggregate.Specifications;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.PermissionRepositories;

public class PermissionReadOnlyRepository : IPermissionReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationPermission> _permissionReadOnlyRepository;

    public PermissionReadOnlyRepository(IReadOnlyRepository<ApplicationPermission> permissionReadOnlyRepository,
        IMapper mapper)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName)
    {
        var dbRolePermissions = await GetAllByRoleNameAsync(roleName);

        return dbRolePermissions.Select(permission => permission.Name).ToList();
    }

    public async Task<IEnumerable<Permission>> GetAllByRoleNameAsync(string roleName)
    {
        var permissionRoleNameSpecification = new PermissionRoleNameSpecification(roleName);

        var applicationPermissions = await _permissionReadOnlyRepository.GetAllAsync(permissionRoleNameSpecification);

        return _mapper.Map<IEnumerable<Permission>>(applicationPermissions);
    }
}