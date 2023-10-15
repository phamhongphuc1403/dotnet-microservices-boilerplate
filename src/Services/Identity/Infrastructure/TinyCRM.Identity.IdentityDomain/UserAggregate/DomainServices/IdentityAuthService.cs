using System.Security.Authentication;
using System.Security.Claims;
using BuildingBlock.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.RoleAggregate.DomainServices;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identities.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.Identity.UserAggregate.DomainServices;

public class IdentityAuthService : IAuthService
{
    private readonly IRoleDomainService _roleDomainService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserDomainService _userDomainService;

    public IdentityAuthService(SignInManager<ApplicationUser> signInManager, IUserDomainService userDomainService,
        IRoleDomainService roleDomainService)
    {
        _signInManager = signInManager;
        _userDomainService = userDomainService;
        _roleDomainService = roleDomainService;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = Optional<User>.Of(await _userDomainService.GetByEmailAsync(email))
            .ThrowIfNotPresent(new UserNotFoundException(nameof(email), email)).Get();

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (result.Succeeded) return user;

        throw new AuthenticationException("Email and password doesn't match");
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var roles = await _roleDomainService.GetManyAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}