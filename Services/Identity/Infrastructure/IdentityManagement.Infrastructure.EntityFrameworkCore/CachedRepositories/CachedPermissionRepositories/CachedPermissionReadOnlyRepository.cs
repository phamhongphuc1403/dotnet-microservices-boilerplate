using BuildingBlock.Core.Domain.Shared.Constants;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.PermissionAggregate.Entities;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedPermissionRepositories;

public class CachedPermissionReadOnlyRepository : IPermissionReadOnlyRepository
{
    private readonly ICacheService _cacheService;
    private readonly IPermissionReadOnlyRepository _permissionReadOnlyRepository;

    public CachedPermissionReadOnlyRepository(IPermissionReadOnlyRepository permissionReadOnlyRepository,
        ICacheService cacheService)
    {
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName)
    {
        return await _cacheService.GetOrSetRecordAsync(CacheKeyRegistry.GetPermissionsByRoleNameKey(roleName),
            async () => await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(roleName),
            TimeSpan.FromMinutes(30));
    }

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<Permission>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        return _permissionReadOnlyRepository.GetAllAsync<TDto>(specification, orderBy, includeTables,
            ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<Permission>? specification = null,
        bool ignoreQueryFilters = false)
    {
        return _permissionReadOnlyRepository.CheckIfExistAsync(specification, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<Permission>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        return _permissionReadOnlyRepository.GetFilterAndPagingAsync<TDto>(specification, sort, pageIndex, pageSize,
            includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<Permission>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        return _permissionReadOnlyRepository.GetAnyAsync<TDto>(specification, includeTables, ignoreQueryFilters);
    }

    public Task<Permission?> GetAnyAsync(ISpecification<Permission>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false)
    {
        return _permissionReadOnlyRepository.GetAnyAsync(specification, includeTables, ignoreQueryFilters, track);
    }

    public Task<List<Permission>> GetAllAsync(ISpecification<Permission>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        return _permissionReadOnlyRepository.GetAllAsync(specification, orderBy, includeTables, ignoreQueryFilters,
            track);
    }

    public Task<(List<Permission>, int)> GetFilterAndPagingAsync(ISpecification<Permission>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        return _permissionReadOnlyRepository.GetFilterAndPagingAsync(specification, sort, pageIndex, pageSize,
            includeTables, ignoreQueryFilters);
    }
}