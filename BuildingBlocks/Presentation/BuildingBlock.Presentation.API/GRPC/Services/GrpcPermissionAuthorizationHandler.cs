using System.Security.Claims;
using BuildingBlock.API.GRPC;
using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Shared.Constants;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Presentation.API.Authorization;
using Microsoft.AspNetCore.Authorization;

namespace BuildingBlock.Presentation.API.GRPC.Services;

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
            // if (!await IsUserEmailConfirmedAsync(userId))
            //     throw new UnauthorizedException("User email is not confirmed.");

            var permissions = await GetPermissionsAsync(userId);

            if (!permissions.Contains(requirement.Permission)) throw new UnauthorizedException();

            context.Succeed(requirement);
        }
    }

    private async Task<bool> IsUserEmailConfirmedAsync(string userId)
    {
        var cachedUserEmailConfirmation =
            await _cacheService.GetRecordAsync<bool?>(CacheKeyRegistry.GetEmailConfirmationByUserIdKey(userId));

        if (cachedUserEmailConfirmation != null) return cachedUserEmailConfirmation.Value;

        var grpcUserEmailConfirmationResponse =
            _authProviderClient.CheckEmailConfirmationAsync(new EmailConfirmationRequest { UserId = userId });

        return grpcUserEmailConfirmationResponse.IsConfirmed;
    }

    private async Task<IEnumerable<string>?> GetCachedPermissionsAsync(string userId)
    {
        var roles =
            await _cacheService.GetRecordAsync<IEnumerable<string>>(CacheKeyRegistry.GetRolesByUserIdKey(userId));

        if (roles == null) return null;

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions =
                await _cacheService.GetRecordAsync<IEnumerable<string>>(
                    CacheKeyRegistry.GetPermissionsByRoleNameKey(role));

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