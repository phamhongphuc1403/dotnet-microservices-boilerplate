using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Application.Permissions.DTOs;
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
        var permissionRoleNameSpecification = new PermissionRoleNameSpecification(roleName);

        var permissionNames =
            await _permissionReadOnlyRepository.GetAllAsync<PermissionNameDto>(permissionRoleNameSpecification);

        return permissionNames.Select(permissionName => permissionName.Name);
    }

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<Permission>? specification = null, string? orderBy = null,
        string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        return _permissionReadOnlyRepository.GetAllAsync<TDto>(applicationPermissionSpecification, orderBy,
            includeTables,
            ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<Permission>? specification = null,
        bool ignoreQueryFilters = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        return _permissionReadOnlyRepository.CheckIfExistAsync(applicationPermissionSpecification, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<Permission>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        return _permissionReadOnlyRepository.GetFilterAndPagingAsync<TDto>(applicationPermissionSpecification, sort,
            pageIndex, pageSize, includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<Permission>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        return _permissionReadOnlyRepository.GetAnyAsync<TDto>(applicationPermissionSpecification, includeTables,
            ignoreQueryFilters);
    }

    public async Task<Permission?> GetAnyAsync(ISpecification<Permission>? specification = null,
        string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        var permission = await _permissionReadOnlyRepository.GetAnyAsync(applicationPermissionSpecification,
            includeTables,
            ignoreQueryFilters, track);

        return _mapper.Map<Permission?>(permission);
    }

    public async Task<List<Permission>> GetAllAsync(ISpecification<Permission>? specification = null,
        string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        var permissions = await _permissionReadOnlyRepository.GetAllAsync(applicationPermissionSpecification, orderBy,
            includeTables, ignoreQueryFilters, track);

        return _mapper.Map<List<Permission>>(permissions);
    }

    public async Task<(List<Permission>, int)> GetFilterAndPagingAsync(ISpecification<Permission>? specification,
        string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationPermissionSpecification = specification?.ConvertTo<ApplicationPermission>(_mapper);

        var (permissions, totalCount) = await _permissionReadOnlyRepository.GetFilterAndPagingAsync(
            applicationPermissionSpecification, sort,
            pageIndex, pageSize, includeTables, ignoreQueryFilters);

        return (_mapper.Map<List<Permission>>(permissions), totalCount);
    }
}