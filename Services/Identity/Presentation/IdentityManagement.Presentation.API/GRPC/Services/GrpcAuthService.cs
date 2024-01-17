using BuildingBlock.API.GRPC;
using BuildingBlock.Core.Application;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using Grpc.Core;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.PermissionAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Presentation.API.GRPC.Services;

public class GrpcAuthService : AuthProvider.AuthProviderBase
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;
    private readonly IPermissionReadOnlyRepository _permissionReadOnlyRepository;
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public GrpcAuthService(ICurrentUser currentUser, IAuthService authService,
        IUserReadOnlyRepository userReadOnlyRepository, IRoleReadOnlyRepository roleReadOnlyRepository,
        IPermissionReadOnlyRepository permissionReadOnlyRepository)
    {
        _currentUser = currentUser;
        _authService = authService;
        _userReadOnlyRepository = userReadOnlyRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
        _permissionReadOnlyRepository = permissionReadOnlyRepository;
    }

    public override async Task GetClaimsAsync(ClaimRequest claimRequest,
        IServerStreamWriter<ClaimResponse> responseStream,
        ServerCallContext context)
    {
        if (!_currentUser.IsAuthenticated)
            throw new RpcException(new Status(StatusCode.Unauthenticated, "User is not authenticated"));

        var userIdSpecification = new EntityIdSpecification<User>(_currentUser.Id);

        var user = await _userReadOnlyRepository.GetAnyAsync(userIdSpecification);

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
        var userId = new Guid(permissionRequest.UserId);

        var userIdSpecification = new EntityIdSpecification<User>(userId);

        Optional<bool>.Of(await _userReadOnlyRepository.CheckIfExistAsync(userIdSpecification))
            .ThrowIfNotExist(new RpcException(new Status(StatusCode.NotFound, "User not found!"))).Get();

        var roles = await _roleReadOnlyRepository.GetNameByUserIdAsync(userId);

        var permissions = new List<string>();

        foreach (var role in roles)
        {
            var rolePermissions = await _permissionReadOnlyRepository.GetNamesByRoleNameAsync(role);

            permissions.AddRange(rolePermissions);
        }

        return new PermissionResponse { Permissions = { permissions } };
    }

    public override async Task<EmailConfirmationResponse> CheckEmailConfirmationAsync(
        EmailConfirmationRequest emailVerificationRequest, ServerCallContext context)
    {
        var userIdSpecification = new EntityIdSpecification<User>(new Guid(emailVerificationRequest.UserId));

        var userEmailConfirmedDto = Optional<UserEmailConfirmedDto>
            .Of(await _userReadOnlyRepository.GetAnyAsync<UserEmailConfirmedDto>(userIdSpecification))
            .ThrowIfNotExist(new RpcException(new Status(StatusCode.NotFound, "User not found!"))).Get();

        return new EmailConfirmationResponse { IsConfirmed = userEmailConfirmedDto.EmailConfirmed };
    }
}