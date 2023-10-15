using AutoMapper;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Identity.Common.Services.Abstractions;
using TinyCRM.Identity.Identity.UserAggregate.Entities;
using TinyCRM.Identity.Identity.UserAggregate.Specifications;

namespace TinyCRM.Identity.Identity.UserAggregate.DomainServices;

public class IdentityUserDomainService : IUserDomainService
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadOnlyRepository<ApplicationUser> _userReadOnlyRepository;

    public IdentityUserDomainService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IIdentityService identityService, IReadOnlyRepository<ApplicationUser> userReadOnlyRepository)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);

        return _mapper.Map<User>(applicationUser);
    }

    public async Task<User?> GetByIdAsync(Guid id)
    {
        var user = await _userManager.FindByIdAsync(id.ToString());

        return _mapper.Map<User>(user);
    }

    public async Task AddRefreshTokenAsync(User user, string refreshToken)
    {
        var applicationUser = await _identityService.GetApplicationUserByIdAsync(user.Id);

        var applicationRefreshToken = new ApplicationRefreshToken(applicationUser.Id, refreshToken);

        applicationUser.RefreshTokens.Add(applicationRefreshToken);

        var result = await _userManager.UpdateAsync(applicationUser);

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }

    public async Task<User> RevokeRefreshToken(Guid userId, string refreshToken)
    {
        var applicationUser = await _identityService.GetApplicationUserByIdAsync(userId);

        var existingRefreshToken = VerifyTokenInDatabase(applicationUser, refreshToken);

        existingRefreshToken.Revoke();

        var result = await _userManager.UpdateAsync(applicationUser);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        return _mapper.Map<User>(applicationUser);
    }

    public async Task<(IEnumerable<User>, int)> FilterAndPagingUsers(string keyword, string sort, int pageIndex,
        int pageSize)
    {
        var specification = new UserEmailPartialMatchSpecification(keyword);

        var (applicationUsers, totalCount) = await _userReadOnlyRepository.GetFilterAndPagingAsync(specification, sort,
            pageIndex, pageSize);

        var user = _mapper.Map<IEnumerable<User>>(applicationUsers);

        return (user, totalCount);
    }

    private static ApplicationRefreshToken VerifyTokenInDatabase(ApplicationUser user, string refreshToken)
    {
        var existingRefreshToken = Optional<ApplicationRefreshToken>
            .Of(user.RefreshTokens.FirstOrDefault(rt => rt.Token == refreshToken))
            .ThrowIfNotPresent(new Exception("token not found")).Get();

        if (existingRefreshToken.RevokedAt != null) throw new Exception("Token is already revoked");

        return existingRefreshToken;
    }
}