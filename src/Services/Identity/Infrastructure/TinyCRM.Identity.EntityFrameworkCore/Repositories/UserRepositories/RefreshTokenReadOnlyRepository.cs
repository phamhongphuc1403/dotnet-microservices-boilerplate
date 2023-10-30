using AutoMapper;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.UserAggregate.Specifications;

namespace TinyCRM.Identity.EntityFrameworkCore.Repositories.UserRepositories;

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

    public async Task<IEnumerable<RefreshToken>> GetByUserId(Guid userId)
    {
        var refreshTokenUserIdSpecification = new RefreshTokenUserIdSpecification(userId);

        var refreshTokenRevokedAtIsNullSpecification = new RefreshTokenRevokedAtIsNullSpecification();

        var specification = refreshTokenUserIdSpecification.And(refreshTokenRevokedAtIsNullSpecification);

        var refreshTokens = await _refreshTokenReadOnlyRepository.GetAllAsync(specification);

        return _mapper.Map<IEnumerable<RefreshToken>>(refreshTokens);
    }
}