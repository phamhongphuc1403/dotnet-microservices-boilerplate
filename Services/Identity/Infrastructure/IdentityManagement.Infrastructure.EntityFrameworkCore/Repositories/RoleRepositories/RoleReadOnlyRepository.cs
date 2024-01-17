using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
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

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<Role>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        return _roleReadOnlyRepository.GetAllAsync<TDto>(applicationRoleSpecification, orderBy, includeTables,
            ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<Role>? specification = null, bool ignoreQueryFilters = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        return _roleReadOnlyRepository.CheckIfExistAsync(applicationRoleSpecification, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<Role>? specification, string sort,
        int pageIndex, int pageSize,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        return _roleReadOnlyRepository.GetFilterAndPagingAsync<TDto>(applicationRoleSpecification, sort, pageIndex,
            pageSize, includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<Role>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        return _roleReadOnlyRepository.GetAnyAsync<TDto>(applicationRoleSpecification, includeTables,
            ignoreQueryFilters);
    }

    public async Task<Role?> GetAnyAsync(ISpecification<Role>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        var role = await _roleReadOnlyRepository.GetAnyAsync(applicationRoleSpecification);

        return _mapper.Map<Role>(role);
    }

    public async Task<List<Role>> GetAllAsync(ISpecification<Role>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        var roles = await _roleReadOnlyRepository.GetAllAsync(applicationRoleSpecification, orderBy, includeTables,
            ignoreQueryFilters, track);

        return _mapper.Map<List<Role>>(roles);
    }

    public async Task<(List<Role>, int)> GetFilterAndPagingAsync(ISpecification<Role>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRoleSpecification = specification?.ConvertTo<ApplicationRole>(_mapper);

        var (roles, totalCount) = await _roleReadOnlyRepository.GetFilterAndPagingAsync(applicationRoleSpecification,
            sort, pageIndex, pageSize, includeTables, ignoreQueryFilters);

        return (_mapper.Map<List<Role>>(roles), totalCount);
    }
}