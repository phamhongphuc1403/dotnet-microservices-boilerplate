using BuildingBlock.Core.Domain.Shared.Services;
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
        return await _cacheService.GetOrSetRecordAsync(userId.ToString(),
            async () => await _roleReadOnlyRepository.GetNameByUserIdAsync(userId), TimeSpan.FromMinutes(30));
    }

    public Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId)
    {
        return _roleReadOnlyRepository.GetByUserIdAsync(userId);
    }

    public Task<Role?> GetByNameAsync(string roleName, string? includeTables = null)
    {
        return _roleReadOnlyRepository.GetByNameAsync(roleName, includeTables);
    }
}