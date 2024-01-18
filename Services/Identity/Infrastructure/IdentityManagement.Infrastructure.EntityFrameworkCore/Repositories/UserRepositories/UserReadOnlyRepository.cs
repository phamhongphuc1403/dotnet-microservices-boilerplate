using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.UserRepositories;

public class UserReadOnlyRepository : IUserReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadOnlyRepository<ApplicationUser> _userReadOnlyRepository;

    public UserReadOnlyRepository(IReadOnlyRepository<ApplicationUser> userReadOnlyRepository, IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
        _userManager = userManager;
    }

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<User>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationUserSpecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        return _userReadOnlyRepository.GetAllAsync<TDto>(applicationUserSpecification, orderBy, includeTables,
            ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<User>? specification = null, bool ignoreQueryFilters = false)
    {
        var applicationUserSpecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        return _userReadOnlyRepository.CheckIfExistAsync(applicationUserSpecification, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<User>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationUserSpecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        return _userReadOnlyRepository.GetFilterAndPagingAsync<TDto>(applicationUserSpecification, sort, pageIndex,
            pageSize, includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<User>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false)
    {
        var applicationUserSpecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        return _userReadOnlyRepository.GetAnyAsync<TDto>(applicationUserSpecification, includeTables,
            ignoreQueryFilters);
    }

    public async Task<User?> GetAnyAsync(ISpecification<User>? specification = null, string? includeTables = null,
        bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationUserspecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        var user = await _userReadOnlyRepository.GetAnyAsync(applicationUserspecification, includeTables,
            ignoreQueryFilters, track);

        return _mapper.Map<User>(user);
    }

    public async Task<List<User>> GetAllAsync(ISpecification<User>? specification = null, string? orderBy = null,
        string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationUserspecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        var users = await _userReadOnlyRepository.GetAllAsync(applicationUserspecification, orderBy, includeTables,
            ignoreQueryFilters, track);

        return _mapper.Map<List<User>>(users);
    }

    public async Task<(List<User>, int)> GetFilterAndPagingAsync(ISpecification<User>? specification, string sort,
        int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationUserspecification = specification?.ConvertTo<ApplicationUser>(_mapper);

        var (users, totalCount) = await _userReadOnlyRepository.GetFilterAndPagingAsync(applicationUserspecification,
            sort, pageIndex, pageSize, includeTables, ignoreQueryFilters);

        return (_mapper.Map<List<User>>(users), totalCount);
    }

    public Task<bool> CheckPasswordAsync(User user, string password)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        return _userManager.CheckPasswordAsync(applicationUser, password);
    }

    public Task<string> GeneratePasswordResetTokenAsync(User user)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        return _userManager.GeneratePasswordResetTokenAsync(applicationUser);
    }

    public Task<string> GenerateEmailConfirmationTokenAsync(User user)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        return _userManager.GenerateEmailConfirmationTokenAsync(applicationUser);
    }
}