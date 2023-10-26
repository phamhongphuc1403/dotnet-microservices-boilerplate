using AutoMapper;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Domain.PermissionAggregate.Entities;
using TinyCRM.Identity.Domain.PermissionAggregate.Repositories;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.PermissionAggregate.Specifications;

namespace TinyCRM.Identity.EntityFrameworkCore.Repositories.PermissionRepositories;

public class PermissionReadOnlyRepository : IPermissionReadOnlyRepository
{
    private readonly ICacheService _cacheService;
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationPermission> _permissionReadOnlyRepository;

    public PermissionReadOnlyRepository(IReadOnlyRepository<ApplicationPermission> permissionReadOnlyRepository,
        IMapper mapper, ICacheService permissionCacheService)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
        _mapper = mapper;
        _cacheService = permissionCacheService;
    }

    public async Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName)
    {
        return await _cacheService.GetOrSetRecordAsync<IEnumerable<string>>(roleName, async () =>
        {
            var dbRolePermissions = await GetAllByRoleNameAsync(roleName);

            return dbRolePermissions.Select(permission => permission.Name).ToList();
        }, TimeSpan.FromMinutes(30));
    }

    public async Task<IEnumerable<Permission>> GetAllByRoleNameAsync(string roleName)
    {
        var permissionRoleNameSpecification = new PermissionRoleNameSpecification(roleName);

        var applicationPermissions = await _permissionReadOnlyRepository.GetAllAsync(permissionRoleNameSpecification);

        return _mapper.Map<IEnumerable<Permission>>(applicationPermissions);
    }
}