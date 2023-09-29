using BuildingBlock.Application.CQRS;
using TinyCRM.Identities.Application.CQRS.Commands.Requests;
using TinyCRM.Identities.Application.DTOs;
using TinyCRM.Identities.Application.Services.Interfaces;

namespace TinyCRM.Identities.Application.CQRS.Commands.Handlers;

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