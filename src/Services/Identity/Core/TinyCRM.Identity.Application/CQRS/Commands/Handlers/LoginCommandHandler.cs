using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs;
using TinyCRM.Identity.Application.Services.Interfaces;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
{
    private readonly IIdentityService _identityService;
    private readonly ITokenService _tokenService;

    public LoginCommandHandler(IIdentityService identityService, ITokenService tokenService)
    {
        _identityService = identityService;
        _tokenService = tokenService;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var claims = await _identityService.Login(request.Email, request.Password);

        var accessToken = _tokenService.GenerateTokens(claims, 5);

        return new LoginResponseDto
        {
            AccessToken = accessToken
        };
    }
}