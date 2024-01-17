using BuildingBlock.Core.Domain.Shared.Constants;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedRoleRepositories;

public class CachedRoleReadOnlyRepository : IRoleReadOnlyRepository
{
    private readonly ICacheService _cacheService;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;

    public CachedRoleReadOnlyRepository(IRoleReadOnlyRepository roleReadOnlyRepository, ICacheService cacheService)
    {
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _cacheService = cacheService;
    }

    public async Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId)
    {
        return await _cacheService.GetOrSetRecordAsync(CacheKeyRegistry.GetRolesByUserIdKey(userId.ToString()),
            async () => await _roleReadOnlyRepository.GetNameByUserIdAsync(userId), TimeSpan.FromMinutes(30));
    }

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<Role>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        return _roleReadOnlyRepository.GetAllAsync<TDto>(specification, orderBy, includeTables, ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<Role>? specification = null, bool ignoreQueryFilters = false)
    {
        return _roleReadOnlyRepository.CheckIfExistAsync(specification, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<Role>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        return _roleReadOnlyRepository.GetFilterAndPagingAsync<TDto>(specification, sort, pageIndex, pageSize,
            includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<Role>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        return _roleReadOnlyRepository.GetAnyAsync<TDto>(specification, includeTables, ignoreQueryFilters);
    }

    public Task<Role?> GetAnyAsync(ISpecification<Role>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false)
    {
        return _roleReadOnlyRepository.GetAnyAsync(specification, includeTables, ignoreQueryFilters, track);
    }

    public Task<List<Role>> GetAllAsync(ISpecification<Role>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        return _roleReadOnlyRepository.GetAllAsync(specification, orderBy, includeTables, ignoreQueryFilters, track);
    }

    public Task<(List<Role>, int)> GetFilterAndPagingAsync(ISpecification<Role>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        return _roleReadOnlyRepository.GetFilterAndPagingAsync(specification, sort, pageIndex, pageSize, includeTables,
            ignoreQueryFilters);
    }
}