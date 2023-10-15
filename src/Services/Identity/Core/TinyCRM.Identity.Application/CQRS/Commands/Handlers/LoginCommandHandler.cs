using BuildingBlock.Application.CQRS;
using TinyCRM.Identities.Domain.UserAggregate.DomainServices;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IAuthService authService, ITokenService tokenService)
    {
        _authService = authService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.Login(request.Email, request.Password);

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