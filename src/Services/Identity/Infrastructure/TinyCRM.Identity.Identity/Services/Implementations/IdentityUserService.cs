using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.Identity.Services.Implementations;

public class IdentityUserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityUserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
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
}