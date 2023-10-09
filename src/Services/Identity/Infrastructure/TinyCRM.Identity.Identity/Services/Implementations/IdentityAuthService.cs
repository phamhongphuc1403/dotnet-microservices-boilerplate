using System.Security.Authentication;
using System.Security.Claims;
using BuildingBlock.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.Identity.Services.Implementations;

public class IdentityAuthService : IAuthService
{
    private readonly IRoleService _roleService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;

    public IdentityAuthService(SignInManager<ApplicationUser> signInManager, IUserService userService,
        IRoleService roleService)
    {
        _signInManager = signInManager;
        _userService = userService;
        _roleService = roleService;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = Optional<User>.Of(await _userService.GetByEmailAsync(email))
            .ThrowIfNotPresent(new UserNotFoundException(nameof(email), email)).Get();

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (result.Succeeded) return user;

        throw new AuthenticationException("Email and password doesn't match");
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

        var roles = await _roleService.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}