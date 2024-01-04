using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Application.Roles.DTOs;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Specifications;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.RoleRepositories;

public class RoleReadOnlyRepository : IRoleReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationRole> _roleReadOnlyRepository;

    public RoleReadOnlyRepository(IReadOnlyRepository<ApplicationRole> roleReadOnlyRepository, IMapper mapper)
    {
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<string>> GetNameByUserIdAsync(Guid userId)
    {
        var roleUserIdSpecification = new RoleUserIdSpecification(userId);

        var roleNames = await _roleReadOnlyRepository.GetAllAsync<RoleNameDto>(roleUserIdSpecification);

        return roleNames.Select(role => role.Name).ToList();
    }

    public Task<List<Role>> GetByUserIdAsync(Guid userId)
    {
        var roleUserIdSpecification = new RoleUserIdSpecification(userId);

        return _roleReadOnlyRepository.GetAllAsync<Role>(roleUserIdSpecification);
    }

    public async Task<Role?> GetByNameAsync(string roleName, string? includeTables = null)
    {
        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification(roleName);

        var applicationRole = await _roleReadOnlyRepository.GetAnyAsync(roleNameExactMatchSpecification, includeTables);

        return _mapper.Map<Role>(applicationRole);
    }
}