using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs;
using TinyCRM.Identity.Application.Services.Abstractions;

namespace TinyCRM.Identity.Application.CQRS.Commands.Handlers;

public class GenerateRefreshTokenCommandHandler : ICommandHandler<GenerateRefreshTokenCommand, LoginResponseDto>
{
    private readonly ITokenService _tokenService;
    private readonly IUserService _userService;

    public GenerateRefreshTokenCommandHandler(ITokenService tokenService, IUserService userService)
    {
        _tokenService = tokenService;
        _userService = userService;
    }

    public async Task<LoginResponseDto> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        // var claimPrincipal = _tokenService.VerifyRefreshTokenAsync(request.RefreshToken);
        //
        // var userId = claimPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!.Value;
        //
        // var user = await _userService.GetByIdAsync(userId);
        //
        // return new LoginResponseDto
        // {
        //     AccessToken = _tokenService.GenerateAccessToken(claimPrincipal.Claims),
        //     RefreshToken = _tokenService.GenerateRefreshToken(claimPrincipal.Claims, user)
        // };
    }
}