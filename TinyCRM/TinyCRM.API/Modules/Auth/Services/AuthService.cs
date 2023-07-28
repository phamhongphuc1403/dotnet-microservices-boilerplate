using AutoMapper;
using Microsoft.AspNetCore.Identity;
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

            if (user != null)
            {
                var result = await _userManager.CheckPasswordAsync(user, model.Password);

                if (result)
                {
                    return new LoginResponseDTO
                    {
                        AccessToken = await _jwtService.GenerateAccessTokenAsync(user),
                        RefreshToken = _jwtService.GenerateRefreshToken(user)
                    };
                }
            }

            throw new BadRequestException("Email or password does not match");
        }
    }
}
