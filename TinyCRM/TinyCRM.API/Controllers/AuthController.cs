using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Modules.Auth.DTOs;
using TinyCRM.API.Modules.Auth.Services;

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
        public async Task<ActionResult> LoginAsync([FromBody] LoginDTO model)
        {
            return Ok(await _service.LoginAsync(model));
        }
    }

}