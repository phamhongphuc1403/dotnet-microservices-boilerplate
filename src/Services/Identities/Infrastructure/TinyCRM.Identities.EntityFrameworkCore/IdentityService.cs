using System.Security.Authentication;
using System.Security.Claims;
using AutoMapper;
using BuildingBlock.Domain.Utils;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Application.Services.Interfaces;
using TinyCRM.Identities.Domain.Exceptions;
using TinyCRM.Identities.EntityFrameworkCore.Entities;

namespace TinyCRM.Identities.EntityFrameworkCore;

public class IdentityService : IIdentityService
{
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager, IMapper mapper,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task<IEnumerable<Claim>> Login(string email, string password)
    {
        var user = Optional<ApplicationUser>.Of(await _userManager.FindByEmailAsync(email))
            .ThrowIfNotPresent(new UserNotFoundException(nameof(email), email)).Get();

        var result = await _signInManager.PasswordSignInAsync(email, password, false, false);

        if (result.Succeeded)
            return await GetClaimsAsync(user);
        throw new AuthenticationException("Email and password doesn't match");
    }

    private async Task<IEnumerable<Claim>> GetClaimsAsync(ApplicationUser user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id),
            new(ClaimTypes.Email, user.Email!)
        };

        var roles = await _userManager.GetRolesAsync(user);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

        return claims;
    }
}