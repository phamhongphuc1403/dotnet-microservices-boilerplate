using BuildingBlock.Application.CQRS;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class GenerateRefreshTokenCommandHandler : ICommandHandler<GenerateRefreshTokenCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUserDomainService _userDomainService;

    public GenerateRefreshTokenCommandHandler(ITokenService tokenService, IAuthService authService,
        IUserDomainService userDomainService)
    {
        _tokenService = tokenService;
        _authService = authService;
        _userDomainService = userDomainService;
    }

    public async Task<LoginResponseDto> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.VerifyRefreshToken(request.RefreshToken);

        var user = await _userDomainService.RevokeRefreshToken(userId, request.RefreshToken);

        var claims = (await _authService.GetClaimsAsync(user)).ToList();

        var accessToken = _tokenService.GenerateAccessToken(claims);

        var refreshToken = await _tokenService.GenerateRefreshTokenAsync(claims, user);

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}