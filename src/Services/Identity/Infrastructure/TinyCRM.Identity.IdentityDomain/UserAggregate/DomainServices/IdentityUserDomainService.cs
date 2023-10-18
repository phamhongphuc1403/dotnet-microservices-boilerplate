using AutoMapper;
using BuildingBlock.Application;
using BuildingBlock.Domain.Repositories;
using BuildingBlock.Domain.Utils;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Identity.Common.Services.Abstractions;
using TinyCRM.Identity.Identity.UserAggregate.Entities;
using TinyCRM.Identity.Identity.UserAggregate.Specifications;

namespace TinyCRM.Identity.Identity.UserAggregate.DomainServices;

public class IdentityUserDomainService : IUserDomainService
{
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadOnlyRepository<ApplicationUser> _userReadOnlyRepository;

    public IdentityUserDomainService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IIdentityService identityService, IReadOnlyRepository<ApplicationUser> userReadOnlyRepository,
        ICurrentUser currentUser)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
        _userReadOnlyRepository = userReadOnlyRepository;
        _currentUser = currentUser;
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

    public async Task<User> CreateAsync(string email, string password)
    {
        await CheckIfEmailIsExisted(email);

        var user = new ApplicationUser(email, _currentUser.Email);

        var result = await _userManager.CreateAsync(user, password);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        return _mapper.Map<User>(user);
    }

    private async Task CheckIfEmailIsExisted(string email)
    {
        Optional<ApplicationUser>.Of(await _userManager.FindByEmailAsync(email))
            .ThrowIfPresent(new UserConflictException(email));
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