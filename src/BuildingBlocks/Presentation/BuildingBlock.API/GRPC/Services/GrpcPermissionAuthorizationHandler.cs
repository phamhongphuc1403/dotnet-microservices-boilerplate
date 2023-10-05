using System.Security.Claims;
using BuildingBlock.API.Authorization;
using BuildingBlock.Application;
using Microsoft.AspNetCore.Authorization;

namespace BuildingBlock.API.GRPC.Services;

public class GrpcPermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly AuthProvider.AuthProviderClient _authProviderClient;
    private readonly IPermissionCacheService _permissionCacheService;
    private readonly IRoleCacheService _roleCacheService;

    public GrpcPermissionAuthorizationHandler(IPermissionCacheService permissionCacheService,
        IRoleCacheService roleCacheService, AuthProvider.AuthProviderClient authProviderClient)
    {
        _permissionCacheService = permissionCacheService;
        _roleCacheService = roleCacheService;
        _authProviderClient = authProviderClient;
    }

    protected override async Task HandleRequirementAsync(AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userId = context.User.FindFirstValue(ClaimTypes.NameIdentifier);

        if (userId != null)
        {
            var permissions = await GetPermissionsAsync(userId);

            var isAuthorized = permissions.Contains(requirement.Permission);

            if (isAuthorized) context.Succeed(requirement);
        }
    }

    private async Task<List<string>?> GetCachedPermissionsAsync(string userId)
    {
        var roles = await _roleCacheService.GetAsync(userId);

        if (roles == null) return null;

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionCacheService.GetAsync(role);

            if (rolePermissions == null) return null;
            permissions.AddRange(rolePermissions);
        }

        return permissions;
    }

    private async Task<IEnumerable<string>> GetPermissionsAsync(string userId)
    {
        var cachedPermissions = await GetCachedPermissionsAsync(userId);

        if (cachedPermissions != null) return cachedPermissions;

        var grpcPermissionResponse = _authProviderClient.GetPermissionsAsync(new PermissionRequest { UserId = userId });

        return grpcPermissionResponse.Permissions;
    }
}