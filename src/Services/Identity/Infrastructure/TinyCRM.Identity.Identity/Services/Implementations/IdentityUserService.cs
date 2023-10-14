using AutoMapper;
using BuildingBlock.Domain.Utils;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Identity.Entities;
using TinyCRM.Identity.Identity.Services.Abstractions;

namespace TinyCRM.Identity.Identity.Services.Implementations;

public class IdentityUserService : IUserService
{
    private readonly IIdentityService _identityService;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityUserService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IIdentityService identityService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
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

    public Task<IEnumerable<User>> FilterAndPagingUsers(string sort, int pageIndex, int pageSize,
        string? includeTables = null)
    {
        // var query = _userManager.Users.AsQueryable();
        //
        // query = Filter(query, specification);
        //
        // var totalCount = await query.CountAsync();
        //
        // query = Include(query, includeTables);
        //
        // query = Sort(query, sort);
        //
        // query = query.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
        //
        // return (await query.ToListAsync(), totalCount);
        throw new NotImplementedException();
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