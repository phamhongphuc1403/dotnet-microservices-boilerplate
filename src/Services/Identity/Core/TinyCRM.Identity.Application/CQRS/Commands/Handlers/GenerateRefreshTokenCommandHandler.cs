using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class GenerateRefreshTokenCommandHandler : ICommandHandler<GenerateRefreshTokenCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public GenerateRefreshTokenCommandHandler(ITokenService tokenService, IAuthService authService,
        IUserService userService)
    {
        _tokenService = tokenService;
        _authService = authService;
        _userService = userService;
    }

    public async Task<LoginResponseDto> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var userId = _tokenService.VerifyRefreshToken(request.RefreshToken);

        var user = await _userService.RevokeRefreshToken(userId, request.RefreshToken);

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