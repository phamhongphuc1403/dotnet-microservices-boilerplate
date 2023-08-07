using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Modules.Auth.DTOs;
using TinyCRM.Application.Modules.Auth.Services.Interfaces;

namespace TinyCRM.API.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _service;

        public AuthController(IAuthService authService)
        {
            _service = authService;
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponseDto>> LoginAsync([FromBody] LoginDto model)
        {
            return Ok(await _service.LoginAsync(model));
        }

        [HttpPost("refresh-token")]
        public async Task<ActionResult<RefreshTokenResponseDto>> RefreshTokenAsync([FromBody] RefreshTokenDto model)
        {
            return Ok(await _service.RefreshTokenAsync(model));
        }
    }
}