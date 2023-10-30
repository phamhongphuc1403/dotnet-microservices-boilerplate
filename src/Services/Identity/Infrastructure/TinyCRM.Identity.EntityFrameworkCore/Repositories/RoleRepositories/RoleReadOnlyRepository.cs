using AutoMapper;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Specifications;

namespace TinyCRM.Identity.EntityFrameworkCore.Repositories.RoleRepositories;

public class RoleReadOnlyRepository : IRoleReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly ICacheService _roleCacheService;
    private readonly IReadOnlyRepository<ApplicationRole> _roleReadOnlyRepository;


    public RoleReadOnlyRepository(IReadOnlyRepository<ApplicationRole> roleReadOnlyRepository,
        IMapper mapper,
        ICacheService roleCacheService)
    {
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _mapper = mapper;
        _roleCacheService = roleCacheService;
    }

    public async Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId)
    {
        return await _roleCacheService.GetOrSetRecordAsync(userId.ToString(), async () =>
        {
            var applicationRoles = await GetByUserIdAsync(userId);

            return applicationRoles.Select(role => role.Name).ToList();
        }, TimeSpan.FromMinutes(30));
    }

    public async Task<IEnumerable<Role>> GetByUserIdAsync(Guid userId)
    {
        var roleUserIdSpecification = new RoleUserIdSpecification(userId);

        var applicationRoles = await _roleReadOnlyRepository.GetAllAsync(roleUserIdSpecification);

        return _mapper.Map<IEnumerable<Role>>(applicationRoles);
    }

    public async Task<Role?> GetByNameAsync(string roleName, string? includeTables = null)
    {
        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification(roleName);

        var applicationRole = await _roleReadOnlyRepository.GetAnyAsync(roleNameExactMatchSpecification, includeTables);

        return _mapper.Map<Role>(applicationRole);
    }
}