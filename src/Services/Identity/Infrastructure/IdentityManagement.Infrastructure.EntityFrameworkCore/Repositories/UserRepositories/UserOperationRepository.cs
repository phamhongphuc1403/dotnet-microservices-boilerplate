using AutoMapper;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Infrastructure.Identity.Exceptions;
using Identitymanagement.Core.Domain.UserAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Exceptions;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.UserRepositories;

public class UserOperationRepository : IUserOperationRepository
{
    private readonly IMapper _mapper;
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public UserOperationRepository(UserManager<ApplicationUser> userManager, IMapper mapper,
        SignInManager<ApplicationUser> signInManager)
    {
        _userManager = userManager;
        _mapper = mapper;
        _signInManager = signInManager;
    }

    public async Task UpdateAsync(User user)
    {
        var applicationUser = await GetApplicationUserAsync(user);

        var result = await _userManager.UpdateAsync(applicationUser);

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }

    public async Task ChangePasswordAsync(User user, string currentPassword, string newPassword)
    {
        var applicationUser = await GetApplicationUserAsync(user);

        var result = await _userManager.ChangePasswordAsync(applicationUser, currentPassword, newPassword);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        _mapper.Map(applicationUser, user);
    }

    public async Task CreateAsync(User user, string password)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        var result = await _userManager.CreateAsync(applicationUser, password);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        _mapper.Map(applicationUser, user);
    }

    public async Task<bool> PasswordSignInAsync(User user, string password)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        var result = await _signInManager.PasswordSignInAsync(applicationUser, password, false, false);

        return result.Succeeded;
    }

    public async Task ResetPasswordAsync(User user, string token, string newPassword)
    {
        var applicationUser = await GetApplicationUserAsync(user);

        var result = await _userManager.ResetPasswordAsync(applicationUser, token, newPassword);

        if (!result.Succeeded) throw new IdentityException(result.Errors);
    }

    private async Task<ApplicationUser> GetApplicationUserAsync(User user)
    {
        var applicationUser = await _userManager.FindByIdAsync(user.Id.ToString());

        Optional<ApplicationUser>.Of(applicationUser).ThrowIfNotExist(new UserNotFoundException(user.Id));

        _mapper.Map(user, applicationUser);

        return applicationUser!;
    }
}