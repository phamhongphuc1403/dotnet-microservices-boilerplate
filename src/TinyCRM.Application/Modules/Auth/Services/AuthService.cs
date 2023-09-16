using System.Security.Claims;
using TinyCRM.Application.Common.Interfaces;
using TinyCRM.Application.Modules.Auth.DTOs;
using TinyCRM.Application.Modules.Auth.Services.Interfaces;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.Application.Modules.Auth.Services;

public class AuthService : IAuthService
{
    private readonly IIdentityAuthService _identityAuthService;
    private readonly IIdentityService _identityService;
    private readonly IJwtService _jwtService;

    public AuthService(
        IIdentityService identityService,
        IJwtService jwtService,
        IIdentityAuthService identityAuthService)
    {
        _identityService = identityService;
        _jwtService = jwtService;
        _identityAuthService = identityAuthService;
    }

    public async Task<LoginResponseDto> LoginAsync(LoginDto dto)
    {
        var user = await _identityAuthService.AuthenticateUserAsync(dto);

        var refreshToken = await GenerateRefreshTokenAsync(user);

        return new LoginResponseDto
        {
            AccessToken = await _jwtService.GenerateAccessTokenAsync(user),
            RefreshToken = refreshToken
        };
    }

    public async Task<RefreshTokenResponseDto> RefreshTokenAsync(RefreshTokenDto dto)
    {
        var verifiedUser = await VerifyRefreshTokenAsync(dto.RefreshToken);

        var newRefreshToken = await GenerateRefreshTokenAsync(verifiedUser);

        return new RefreshTokenResponseDto
        {
            AccessToken = await _jwtService.GenerateAccessTokenAsync(verifiedUser),
            RefreshToken = newRefreshToken
        };
    }

    private async Task<string> GenerateRefreshTokenAsync(UserEntity user)
    {
        var refreshToken = _jwtService.GenerateRefreshToken(user);

        user.RefreshToken = refreshToken;

        await _identityService.UpdateAsync(user);

        return refreshToken;
    }

    private async Task<UserEntity> VerifyRefreshTokenAsync(string refreshToken)
    {
        var principal = _jwtService.Verify(refreshToken);

        var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier)
                          ?? throw new BadRequestException("Invalid token");

        var userId = userIdClaim.Value;

        var user = await _identityService.GetByIdAsync(userId);

        return user.RefreshToken == refreshToken ? user : throw new BadRequestException("Token has expired");
    }
}