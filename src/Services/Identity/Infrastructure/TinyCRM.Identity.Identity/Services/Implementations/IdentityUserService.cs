using AutoMapper;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Identity.Entities;
using TinyCRM.Identity.Identity.Services.Abstractions;

namespace TinyCRM.Identity.Identity.Services.Implementations;

public class IdentityUserService : IUserService
{
    private readonly IIdentityService _identityService;
    private readonly ILogger<IdentityUserService> _logger;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityUserService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IIdentityService identityService, ILogger<IdentityUserService> logger)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityService = identityService;
        _logger = logger;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);

        return _mapper.Map<User>(applicationUser);
    }

    public async Task<User?> GetByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        return _mapper.Map<User>(user);
    }

    public async Task AddRefreshTokenAsync(User user, string refreshToken)
    {
        var applicationUser = await _identityService.GetApplicationUserByIdAsync(user.Id.ToString());

        var applicationRefreshToken = new ApplicationRefreshToken(applicationUser.Id, refreshToken);

        applicationUser.RefreshTokens.Add(applicationRefreshToken);

        var result = await _userManager.UpdateAsync(applicationUser);

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }


    public async Task CheckIfRefreshTokenIsValidAsync(string userId, string refreshToken)
    {
        var applicationUser = await _identityService.GetApplicationUserByIdAsync(userId);
    }
}