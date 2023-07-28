using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using TinyCRM.API.Modules.Auth.DTOs;
using TinyCRM.Domain.Entities;
using TinyCRM.Domain.HttpExceptions;

namespace TinyCRM.API.Modules.Auth.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<UserEntity> _userManager;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;

        public AuthService(UserManager<UserEntity> userManager, IMapper mapper, IJwtService jwtService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwtService = jwtService;
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginDTO model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var refreshToken = _jwtService.GenerateRefreshToken(user);

                user.RefreshToken = refreshToken;
                await _userManager.UpdateAsync(user);

                return new LoginResponseDTO
                {
                    AccessToken = await _jwtService.GenerateAccessTokenAsync(user),
                    RefreshToken = refreshToken
                };
            }

            throw new BadRequestException("Email or password does not match");
        }

        public async Task<RefreshTokenResponseDTO> RefreshTokenAsync(RefreshTokenDTO model)
        {
            var principal = _jwtService.Verify(model.RefreshToken);

            if (principal == null)
            {
                throw new BadRequestException("Invalid token");
            }

            var userIdClaim = principal.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim != null)
            {
                string userId = userIdClaim.Value;

                var user = await _userManager.FindByIdAsync(userId);

                if (user != null && user.RefreshToken == model.RefreshToken)
                {
                    var refreshToken = _jwtService.GenerateRefreshToken(user);

                    user.RefreshToken = refreshToken;
                    await _userManager.UpdateAsync(user);

                    return new RefreshTokenResponseDTO
                    {
                        AccessToken = await _jwtService.GenerateAccessTokenAsync(user),
                        RefreshToken = refreshToken
                    };
                }
            }
            throw new BadRequestException("Invalid token");
        }
    }
}