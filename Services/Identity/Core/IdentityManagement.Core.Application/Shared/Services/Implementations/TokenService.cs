using System.IdentityModel.Tokens.Jwt;
using System.Security.Authentication;
using System.Security.Claims;
using System.Text;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Application.Shared.Services.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using Microsoft.IdentityModel.Tokens;

namespace IdentityManagement.Core.Application.Shared.Services.Implementations;

public class TokenService : ITokenService
{
    private readonly JwtConfiguration _jwtConfiguration;
    private readonly IRefreshTokenReadOnlyRepository _refreshTokenReadOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public TokenService(JwtConfiguration jwtConfiguration, IUserReadOnlyRepository userReadOnlyRepository,
        IRefreshTokenReadOnlyRepository refreshTokenReadOnlyRepository)
    {
        _jwtConfiguration = jwtConfiguration;
        _userReadOnlyRepository = userReadOnlyRepository;
        _refreshTokenReadOnlyRepository = refreshTokenReadOnlyRepository;
    }

    public string GenerateAccessToken(IEnumerable<Claim> claims)
    {
        return GenerateToken(claims, _jwtConfiguration.AccessTokenLifeTimeInMinute,
            _jwtConfiguration.AccessTokenSecurityKey);
    }

    public string GenerateRefreshToken(IEnumerable<Claim> claims, User user)
    {
        return GenerateToken(claims, _jwtConfiguration.RefreshTokenLifeTimeInMinute,
            _jwtConfiguration.RefreshTokenSecurityKey);
    }

    public async Task<User> VerifyRefreshTokenAsync(string refreshToken)
    {
        ClaimsPrincipal tokenClaimsPrincipal;

        try
        {
            tokenClaimsPrincipal = new JwtSecurityTokenHandler().ValidateToken(refreshToken,
                ValidateToken(_jwtConfiguration.RefreshTokenSecurityKey), out _);
        }
        catch (Exception ex)
        {
            throw new AuthenticationException(ex.Message);
        }

        var user = await GetUserAsync(tokenClaimsPrincipal);

        var existingRefreshToken = Optional<RefreshToken>
            .Of(user.RefreshTokens.FirstOrDefault(rf => rf.Token == refreshToken))
            .ThrowIfNotExist(new AuthenticationException("Token not found")).Get();

        if (existingRefreshToken.RevokedAt != null) throw new AuthenticationException("Token is already revoked");

        existingRefreshToken.Revoke();

        return user;
    }

    public async Task RevokeAllRefreshTokensAsync(User user)
    {
        var refreshTokens = (await _refreshTokenReadOnlyRepository.GetByUserId(user.Id)).ToList();

        foreach (var refreshToken in refreshTokens) refreshToken.Revoke();

        user.RefreshTokens = refreshTokens;
    }

    public TokenValidationParameters ValidateToken(string securityKey)
    {
        return new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = _jwtConfiguration.Issuer,
            ValidAudience = _jwtConfiguration.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(securityKey))
        };
    }

    private async Task<User> GetUserAsync(ClaimsPrincipal tokenClaimsPrincipal)
    {
        var userId = new Guid(tokenClaimsPrincipal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)!
            .Value);

        return Optional<User>.Of(await _userReadOnlyRepository.GetByIdAsync(userId, "RefreshTokens"))
            .ThrowIfNotExist(new AuthenticationException($"user with id: '{userId}' is not found")).Get();
    }

    private string GenerateToken(IEnumerable<Claim> claims, int expireMinutes, string securityKey)
    {
        var key = Encoding.UTF8.GetBytes(securityKey);

        var token = new JwtSecurityToken(
            _jwtConfiguration.Issuer,
            _jwtConfiguration.Audience,
            claims,
            expires: DateTime.UtcNow.AddMinutes(expireMinutes),
            signingCredentials: new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
        );

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}