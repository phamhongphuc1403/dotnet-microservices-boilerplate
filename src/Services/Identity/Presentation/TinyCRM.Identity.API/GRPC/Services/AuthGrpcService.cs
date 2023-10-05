using BuildingBlock.API.GRPC;
using BuildingBlock.Application.Identity;
using Grpc.Core;
using TinyCRM.Identity.Application.Services.Interfaces;

namespace Identities.API.GRPC.Services;

public class AuthGrpcService : AuthProvider.AuthProviderBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;
    private readonly IUserService _userService;

    public AuthGrpcService(ICurrentUser currentUser, IAuthService authService, IUserService userService)
    {
        _currentUser = currentUser;
        _authService = authService;
        _userService = userService;
    }

    public override async Task GetClaims(ClaimRequest claimRequest, IServerStreamWriter<ClaimResponse> responseStream,
        ServerCallContext context)
    {
        if (!_currentUser.IsAuthenticated)
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));

        var userId = _currentUser.Id;

        var user = await _userService.FindByIdAsync(userId!);

        if (user == null) throw new RpcException(new Status(StatusCode.NotFound, "User not found!"));

        var claims = await _authService.GetClaimsAsync(user);

        foreach (var claim in claims)
            await responseStream.WriteAsync(new ClaimResponse
            {
                Type = claim.Type,
                Value = claim.Value
            });
    }
}