using AutoMapper;
using BuildingBlock.Application;
using BuildingBlock.Application.CacheServices.Abstractions;
using BuildingBlock.Domain.Utils;
using BuildingBlocks.Identity.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TinyCRM.Identities.Domain.RoleAggregate.Entities;
using TinyCRM.Identities.Domain.RoleAggregate.Exceptions;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Identity.Entities;

namespace TinyCRM.Identity.Identity.Services.Implementations;

public class IdentityRoleService : IRoleService
{
    private readonly ICurrentUser _currentUser;
    private readonly IMapper _mapper;
    private readonly IRoleCacheService _roleCacheService;
    private readonly RoleManager<ApplicationRole> _roleManager;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityRoleService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IRoleCacheService roleCacheService, RoleManager<ApplicationRole> roleManager, ICurrentUser currentUser)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleCacheService = roleCacheService;
        _roleManager = roleManager;
        _currentUser = currentUser;
    }

    public async Task<IEnumerable<string>> GetManyAsync(User user)
    {
        var cacheUserRoles = await _roleCacheService.GetAsync(user.Id.ToString());

        if (cacheUserRoles != null) return cacheUserRoles;

        var applicationUser = _mapper.Map<ApplicationUser>(user);

        var dbUserRoles = await _userManager.GetRolesAsync(applicationUser);

        await _roleCacheService.SetAsync(user.Id.ToString(), dbUserRoles);

        return dbUserRoles;
    }

    public async Task<Role> CreateAsync(string roleName)
    {
        await CheckValidOnCreate(roleName);

        var applicationRole = new ApplicationRole(roleName, _currentUser.Email);

        var result = await _roleManager.CreateAsync(applicationRole);

        if (!result.Succeeded) throw new IdentityException(result.Errors);

        return _mapper.Map<Role>(applicationRole);
    }

    public async Task<IEnumerable<Role>> GetAllAsync()
    {
        return _mapper.Map<IEnumerable<Role>>(await _roleManager.Roles.ToListAsync());
    }

    private async Task CheckValidOnCreate(string roleName)
    {
        Optional<ApplicationRole>.Of(await _roleManager.FindByNameAsync(roleName))
            .ThrowIfPresent(new RoleConflictException(roleName));
    }
}