using BuildingBlock.API.GRPC;
using BuildingBlock.Application;
using BuildingBlock.Domain.Utils;
using Grpc.Core;
using TinyCRM.Identity.Domain.PermissionAggregate.DomainServices;
using TinyCRM.Identity.Domain.RoleAggregate.DomainServices;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace Identities.API.GRPC.Services;

public class GrpcAuthService : AuthProvider.AuthProviderBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;
    private readonly IPermissionDomainService _permissionDomainService;
    private readonly IRoleDomainService _roleDomainService;
    private readonly IUserDomainService _userDomainService;

    public GrpcAuthService(ICurrentUser currentUser, IAuthService authService,
        IUserDomainService userDomainService, IRoleDomainService roleDomainService,
        IPermissionDomainService permissionDomainService)
    {
        _currentUser = currentUser;
        _authService = authService;
        _userDomainService = userDomainService;
        _roleDomainService = roleDomainService;
        _permissionDomainService = permissionDomainService;
    }

    public override async Task GetClaimsAsync(ClaimRequest claimRequest,
        IServerStreamWriter<ClaimResponse> responseStream,
        ServerCallContext context)
    {
        if (!_currentUser.IsAuthenticated)
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));

        var userId = _currentUser.Id;

        var user = await _userDomainService.GetByIdAsync(userId);

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
        var user = Optional<User>.Of(await _userDomainService.GetByIdAsync(new Guid(permissionRequest.UserId)))
            .ThrowIfNotPresent(new RpcException(new Status(StatusCode.NotFound, "User not found!"))).Get();

        var roles = await _roleDomainService.GetManyAsync(user);

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionDomainService.GetPermissionsAsync(role);

            permissions.AddRange(rolePermissions);
        }

        return new PermissionResponse { Permissions = { permissions } };
    }
}