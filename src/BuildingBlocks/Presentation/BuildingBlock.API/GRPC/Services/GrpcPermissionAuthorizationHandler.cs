using System.Security.Claims;
using BuildingBlock.API.Authorization;
using BuildingBlock.Domain.Shared.Services;
using Microsoft.AspNetCore.Authorization;

namespace BuildingBlock.API.GRPC.Services;

public class GrpcPermissionAuthorizationHandler : AuthorizationHandler<PermissionRequirement>
{
    private readonly AuthProvider.AuthProviderClient _authProviderClient;
    private readonly ICacheService _cacheService;

    public GrpcPermissionAuthorizationHandler(ICacheService cacheService,
        AuthProvider.AuthProviderClient authProviderClient)
    {
        _cacheService = cacheService;
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

    private async Task<IEnumerable<string>?> GetCachedPermissionsAsync(string userId)
    {
        var roles = await _cacheService.GetRecordAsync<IEnumerable<string>>(userId);

        if (roles == null) return null;

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _cacheService.GetRecordAsync<IEnumerable<string>>(role);

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