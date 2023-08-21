using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.EntityFrameworkCore.Identity.Entities;
using TinyCRM.EntityFrameworkCore.Identity.Services.Interfaces;

namespace TinyCRM.EntityFrameworkCore.Identity.Services;

public class IdentityService : IIdentityService
{
    private readonly IIdentityHelper _identityHelper;
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityService(UserManager<ApplicationUser> userManager,
        IMapper mapper,
        IIdentityHelper identityHelper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _identityHelper = identityHelper;
    }

    public async Task<UserEntity> GetByIdAsync(string id)
    {
        var user = await _identityHelper.GetApplicationUserByIdAsync(id);

        return _mapper.Map<UserEntity>(user);
    }

    public async Task<string> CreateAsync(UserEntity user)
    {
        var newUser = _mapper.Map<ApplicationUser>(user);

        var result = await _userManager.CreateAsync(newUser, user.PasswordHash);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.First().Description);

        return newUser.Id;
    }

    public async Task UpdateAsync(UserEntity user)
    {
        var currentUser = await _identityHelper.GetApplicationUserByIdAsync(user.Id.ToString());

        _mapper.Map(user, currentUser);

        var result = await _userManager.UpdateAsync(currentUser);

        if (!result.Succeeded) throw new BadRequestException(result.Errors.First().Description);
    }

    public async Task<(List<UserEntity>, int)> GetAllAsync(UserQueryDto query)
    {
        var users = await _userManager.Users.ToListAsync();

        return (_mapper.Map<List<UserEntity>>(users), users.Count);
    }
}