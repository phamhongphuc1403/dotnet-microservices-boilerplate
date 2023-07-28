using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Principal;
using TinyCRM.API.Constants;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.User.DTOs;
using TinyCRM.API.Modules.User.Services;

namespace TinyCRM.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize(Roles = Role.Admin)]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
        }
        [HttpPost]
        public async Task<ActionResult<GetUserDTO>> CreateAsync([FromBody] CreateUserDTO model)
        {
            var newUser = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id:guid}")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetAccountDTO>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }
    }
}
