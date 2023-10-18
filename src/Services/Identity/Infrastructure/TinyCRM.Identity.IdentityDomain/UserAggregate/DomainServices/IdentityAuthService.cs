using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using BuildingBlock.Domain.Utils;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Identity.Common.Services.Abstractions;
using TinyCRM.Identity.Identity.UserAggregate.Entities;

namespace TinyCRM.Identity.Identity.UserAggregate.DomainServices;

public class IdentityAuthService : IAuthService
{
    private readonly IIdentityService _identityService;
    private readonly IRoleDomainService _roleDomainService;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserDomainService _userDomainService;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityAuthService(SignInManager<ApplicationUser> signInManager, IUserDomainService userDomainService,
        IRoleDomainService roleDomainService, UserManager<ApplicationUser> userManager,
        IIdentityService identityService)
    {
        _signInManager = signInManager;
        _userDomainService = userDomainService;
        _roleDomainService = roleDomainService;
        _userManager = userManager;
        _identityService = identityService;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = await _userDomainService.GetByEmailAsync(email);

        if (user == null) throw new ValidationException("Invalid email or password");

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (result.Succeeded) return user;

        throw new ValidationException("Invalid email or password");
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

    public async Task ChangePasswordAsync(Guid userId, string currentPassword, string newPassword)
    {
        var applicationUser = await _identityService.GetApplicationUserByIdAsync(userId);

        await CheckIfCurrentPasswordIsValid(applicationUser, currentPassword);

        if (currentPassword == newPassword)
            throw new ValidationException("New password must be different from current password");

        var result = await _userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }

    private async Task CheckIfCurrentPasswordIsValid(ApplicationUser applicationUser, string currentPassword)
    {
        Optional<bool>.Of(await _userManager.CheckPasswordAsync(applicationUser, currentPassword))
            .ThrowIfNotPresent(new ValidationException("Invalid password"));
    }
}