using BuildingBlock.API.GRPC;
using BuildingBlock.Application;
using BuildingBlock.Domain.Utils;
using Grpc.Core;
using TinyCRM.Identities.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace Identities.API.GRPC.Services;

public class GrpcAuthService : AuthProvider.AuthProviderBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;
    private readonly IPermissionService _permissionService;
    private readonly IRoleService _roleService;
    private readonly IUserService _userService;

    public GrpcAuthService(ICurrentUser currentUser, IAuthService authService,
        IUserService userService, IRoleService roleService,
        IPermissionService permissionService)
    {
        _currentUser = currentUser;
        _authService = authService;
        _userService = userService;
        _roleService = roleService;
        _permissionService = permissionService;
    }

    public override async Task GetClaimsAsync(ClaimRequest claimRequest,
        IServerStreamWriter<ClaimResponse> responseStream,
        ServerCallContext context)
    {
        if (!_currentUser.IsAuthenticated)
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));

        var userId = _currentUser.Id;

        var user = await _userService.GetByIdAsync(userId!);

        if (user == null) throw new RpcException(new Status(StatusCode.NotFound, "User not found!"));

        var claims = await _authService.GetClaimsAsync(user);

        foreach (var claim in claims)
            await responseStream.WriteAsync(new ClaimResponse
            {
                Type = claim.Type,
                Value = claim.Value
            });
    }


    public override async Task<PermissionResponse> GetPermissionsAsync(PermissionRequest permissionRequest,
        ServerCallContext context)
    {
        var user = Optional<User>.Of(await _userService.GetByIdAsync(permissionRequest.UserId))
            .ThrowIfNotPresent(new RpcException(new Status(StatusCode.NotFound, "User not found!"))).Get();

        var roles = await _roleService.GetRolesAsync(user);

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionService.GetPermissionsAsync(role);

            permissions.AddRange(rolePermissions);
        }

        return new PermissionResponse { Permissions = { permissions } };
    }
}