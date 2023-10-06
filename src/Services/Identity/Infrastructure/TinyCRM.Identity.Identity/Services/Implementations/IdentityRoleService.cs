using AutoMapper;
using BuildingBlock.Application.CacheServices.Abstractions;
using Microsoft.AspNetCore.Identity;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;
using TinyCRM.Identity.Indentity.Entities;

namespace TinyCRM.Identity.Indentity.Services.Implementations;

public class IdentityRoleService : IRoleService
{
    private readonly IMapper _mapper;
    private readonly IRoleCacheService _roleCacheService;
    private readonly UserManager<ApplicationUser> _userManager;

    public IdentityRoleService(UserManager<ApplicationUser> userManager, IMapper mapper,
        IRoleCacheService roleCacheService)
    {
        _userManager = userManager;
        _mapper = mapper;
        _roleCacheService = roleCacheService;
    }

    public async Task<IEnumerable<string>> GetRolesAsync(User user)
    {
        var cacheUserRoles = await _roleCacheService.GetAsync(user.Id.ToString());

        if (cacheUserRoles != null) return cacheUserRoles;

        var applicationUser = _mapper.Map<ApplicationUser>(user);

        var dbUserRoles = await _userManager.GetRolesAsync(applicationUser);

        await _roleCacheService.SetAsync(user.Id.ToString(), dbUserRoles);

        return dbUserRoles;
    }
}