using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.RoleRepositories;

public class UserRoleReadOnlyRepository : IUserRoleReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationUserRole> _userRoleReadOnlyRepository;

    public UserRoleReadOnlyRepository(IReadOnlyRepository<ApplicationUserRole> userRoleReadOnlyRepository,
        IMapper mapper)
    {
        _userRoleReadOnlyRepository = userRoleReadOnlyRepository;
        _mapper = mapper;
    }

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<UserRole>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        return _userRoleReadOnlyRepository.GetAllAsync<TDto>(applicationUserRoleSpecification, orderBy, includeTables,
            ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<UserRole>? specification = null, bool ignoreQueryFilters = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        return _userRoleReadOnlyRepository.CheckIfExistAsync(applicationUserRoleSpecification, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<UserRole>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        return _userRoleReadOnlyRepository.GetFilterAndPagingAsync<TDto>(applicationUserRoleSpecification, sort,
            pageIndex, pageSize, includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<UserRole>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        return _userRoleReadOnlyRepository.GetAnyAsync<TDto>(applicationUserRoleSpecification, includeTables,
            ignoreQueryFilters);
    }

    public async Task<UserRole?> GetAnyAsync(ISpecification<UserRole>? specification = null,
        string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        var userRole = await _userRoleReadOnlyRepository.GetAnyAsync(applicationUserRoleSpecification, includeTables,
            ignoreQueryFilters, track);

        return _mapper.Map<UserRole?>(userRole);
    }

    public async Task<List<UserRole>> GetAllAsync(ISpecification<UserRole>? specification = null,
        string? orderBy = null, string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        var userRoles = await _userRoleReadOnlyRepository.GetAllAsync(applicationUserRoleSpecification, orderBy,
            includeTables, ignoreQueryFilters, track);

        return _mapper.Map<List<UserRole>>(userRoles);
    }

    public async Task<(List<UserRole>, int)> GetFilterAndPagingAsync(ISpecification<UserRole>? specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationUserRoleSpecification = specification?.ConvertTo<ApplicationUserRole>(_mapper);

        var (userRoles, totalCount) = await _userRoleReadOnlyRepository.GetFilterAndPagingAsync(
            applicationUserRoleSpecification, sort, pageIndex, pageSize, includeTables, ignoreQueryFilters);

        return (_mapper.Map<List<UserRole>>(userRoles), totalCount);
    }
}