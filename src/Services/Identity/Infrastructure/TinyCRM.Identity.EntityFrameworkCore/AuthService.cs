using System.Security.Authentication;
using System.Security.Claims;
using BuildingBlock.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.Entities;
using TinyCRM.Identities.Domain.Exceptions;
using TinyCRM.Identity.Application.Services.Interfaces;
using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public class AuthService : IAuthService
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly IUserService _userService;

    public AuthService(SignInManager<ApplicationUser> signInManager, IUserService userService)
    {
        _signInManager = signInManager;
        _userService = userService;
    }

    public async Task<IEnumerable<Claim>> Login(string email, string password)
    {
        var user = Optional<User>.Of(await _userService.GetByEmailAsync(email))
            .ThrowIfNotPresent(new UserNotFoundException(nameof(email), email)).Get();

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (result.Succeeded)
            return await GetClaimsAsync(user);
        throw new AuthenticationException("Email and password doesn't match");
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var roles = await _userService.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}