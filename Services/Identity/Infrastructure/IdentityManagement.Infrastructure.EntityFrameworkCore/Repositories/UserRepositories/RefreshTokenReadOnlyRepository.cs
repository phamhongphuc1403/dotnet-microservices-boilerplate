using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.UserRepositories;

public class RefreshTokenReadOnlyRepository : IRefreshTokenReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationRefreshToken> _refreshTokenReadOnlyRepository;

    public RefreshTokenReadOnlyRepository(
        IReadOnlyRepository<ApplicationRefreshToken> refreshTokenReadOnlyRepository, IMapper mapper)
    {
        _refreshTokenReadOnlyRepository = refreshTokenReadOnlyRepository;
        _mapper = mapper;
    }

    public Task<List<TDto>> GetAllAsync<TDto>(ISpecification<RefreshToken>? specification = null,
        string? orderBy = null, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        return _refreshTokenReadOnlyRepository.GetAllAsync<TDto>(applicationRefreshToken, orderBy, includeTables,
            ignoreQueryFilters);
    }

    public Task<bool> CheckIfExistAsync(ISpecification<RefreshToken>? specification = null,
        bool ignoreQueryFilters = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        return _refreshTokenReadOnlyRepository.CheckIfExistAsync(applicationRefreshToken, ignoreQueryFilters);
    }

    public Task<(List<TDto>, int)> GetFilterAndPagingAsync<TDto>(ISpecification<RefreshToken>? specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        return _refreshTokenReadOnlyRepository.GetFilterAndPagingAsync<TDto>(applicationRefreshToken, sort, pageIndex,
            pageSize, includeTables, ignoreQueryFilters);
    }

    public Task<TDto?> GetAnyAsync<TDto>(ISpecification<RefreshToken>? specification = null,
        string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        return _refreshTokenReadOnlyRepository.GetAnyAsync<TDto>(applicationRefreshToken, includeTables,
            ignoreQueryFilters);
    }

    public async Task<RefreshToken?> GetAnyAsync(ISpecification<RefreshToken>? specification = null,
        string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        var role = await _refreshTokenReadOnlyRepository.GetAnyAsync(applicationRefreshToken, includeTables,
            ignoreQueryFilters, track);

        return _mapper.Map<RefreshToken>(role);
    }

    public async Task<List<RefreshToken>> GetAllAsync(ISpecification<RefreshToken>? specification = null,
        string? orderBy = null, string? includeTables = null, bool ignoreQueryFilters = false, bool track = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        var refreshToken = await _refreshTokenReadOnlyRepository.GetAllAsync(applicationRefreshToken,
            orderBy, includeTables, ignoreQueryFilters, track);

        return _mapper.Map<List<RefreshToken>>(refreshToken);
    }

    public async Task<(List<RefreshToken>, int)> GetFilterAndPagingAsync(ISpecification<RefreshToken>? specification,
        string sort, int pageIndex, int pageSize, string? includeTables = null, bool ignoreQueryFilters = false)
    {
        var applicationRefreshToken = specification?.ConvertTo<ApplicationRefreshToken>(_mapper);

        var (refreshTokens, totalCount) = await _refreshTokenReadOnlyRepository.GetFilterAndPagingAsync(
            applicationRefreshToken, sort, pageIndex,
            pageSize, includeTables, ignoreQueryFilters);

        return (_mapper.Map<List<RefreshToken>>(refreshTokens), totalCount);
    }
}