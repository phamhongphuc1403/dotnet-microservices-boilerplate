using AutoMapper;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.Entities;
using TinyCRM.Identity.Application.Services.Interfaces;
using TinyCRM.Identity.EntityFrameworkCore.Entities;

namespace TinyCRM.Identity.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserService(UserManager<ApplicationUser> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
    }

    public async Task<User?> GetByEmailAsync(string email)
    {
        var applicationUser = await _userManager.FindByEmailAsync(email);

        return _mapper.Map<User>(applicationUser);
    }

    public Task<IList<string>> GetRolesAsync(User user)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        return _userManager.GetRolesAsync(applicationUser);
    }

    public async Task<User?> FindByIdAsync(string id)
    {
        var user = await _userManager.FindByIdAsync(id);

        return _mapper.Map<User>(user);
    }
}