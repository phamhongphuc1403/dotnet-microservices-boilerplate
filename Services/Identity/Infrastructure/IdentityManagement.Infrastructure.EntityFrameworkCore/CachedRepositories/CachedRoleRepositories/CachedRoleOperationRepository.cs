using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.CachedRepositories.CachedRoleRepositories;

public class CachedRoleOperationRepository : IRoleOperationRepository
{
    private readonly ICacheService _cacheService;
    private readonly IRoleOperationRepository _roleOperationRepository;

    public CachedRoleOperationRepository(IRoleOperationRepository roleOperationRepository, ICacheService cacheService)
    {
        _roleOperationRepository = roleOperationRepository;
        _cacheService = cacheService;
    }

    public Task CreateAsync(Role role)
    {
        return _roleOperationRepository.CreateAsync(role);
    }

    public async Task UpdateAsync(Role role)
    {
        await _roleOperationRepository.UpdateAsync(role);

        await _cacheService.RemoveRecordAsync(role.Name);
    }
}