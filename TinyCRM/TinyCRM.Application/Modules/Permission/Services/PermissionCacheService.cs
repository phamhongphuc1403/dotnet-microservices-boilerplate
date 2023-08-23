using Microsoft.Extensions.Configuration;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Common.Params;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;
using TinyCRM.RedisCache;

namespace TinyCRM.Application.Modules.Permission.Services;

public class PermissionCacheService : IPermissionCacheService
{
    private readonly ICacheService _cacheService;
    private readonly JwtParams _jwtParams;
    private readonly IIdentityRoleService _identityRoleService;

    public PermissionCacheService(
        ICacheService cacheService,
        IConfiguration configuration,
        IIdentityRoleService identityRoleService
    )
    {
        var jwtSettings = configuration.GetSection("JwtSettings");
        _jwtParams = new JwtParams(jwtSettings);
        _cacheService = cacheService;
        _identityRoleService = identityRoleService;
    }

    public Task<IEnumerable<PermissionEntity>?> GetAllByRoleNameAsync(string roleName)
    {
        return _cacheService.GetRecordAsync<IEnumerable<PermissionEntity>?>(roleName);
    }

    public async Task<IEnumerable<PermissionEntity>> AddByRoleNameAsync(string roleName)
    {
        var permissions = (await _identityRoleService.GetAllPermissionsByRoleName(roleName)).ToList();

        var result =
            await _cacheService.SetRecordAsync(roleName, permissions, TimeSpan.FromMinutes(_jwtParams.ExpireMinute));

        return result ? permissions : throw new InternalException("Error when adding permissions to cache");
    }

    public async Task<IEnumerable<PermissionEntity>> GetAllOrAddByRoleName(string roleName)
    {
        var permissionsFromCache = await GetAllByRoleNameAsync(roleName);

        if (permissionsFromCache == null)
        {
            var permissionsFormDb = await AddByRoleNameAsync(roleName);

            return permissionsFormDb;
        }

        return permissionsFromCache;
    }

    public async Task RemoveByRoleNameAsync(string roleName)
    {
        var permissions = await GetAllByRoleNameAsync(roleName);

        if (permissions == null)
        {
            return;
        }

        var result = await _cacheService.RemoveRecordAsync(roleName);

        if (!result)
        {
            throw new InternalException("Error when removing permissions from cache");
        }
    }
}