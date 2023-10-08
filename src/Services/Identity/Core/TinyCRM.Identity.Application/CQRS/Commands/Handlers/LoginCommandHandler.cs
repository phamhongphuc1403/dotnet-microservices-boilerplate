using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs;
using TinyCRM.Identity.Application.Services.Abstractions;

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
        var claims = await _authService.Login(request.Email, request.Password);

        var accessToken = _tokenService.GenerateAccessToken(claims);

        return new LoginResponseDto
        {
            AccessToken = accessToken
        };
    }
}