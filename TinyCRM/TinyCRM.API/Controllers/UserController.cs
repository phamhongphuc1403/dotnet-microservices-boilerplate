using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Modules.Account.DTOs;
using TinyCRM.Application.Modules.User.DTOs;
using TinyCRM.Application.Modules.User.Services.Interfaces;
using TinyCRM.Domain.Constants;

namespace TinyCRM.API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _service;

        public UserController(IUserService userService)
        {
            _service = userService;
        }

        [HttpPost]
        [Authorize(Roles = Role.Admin)]
        public async Task<ActionResult<GetUserDto>> CreateAsync([FromBody] CreateOrEditUserDto model)
        {
            var newUser = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "ViewAndUpdateUserPermission")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetAccountDto>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id.ToString()));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "ViewAndUpdateUserPermission")]
        public async Task<ActionResult<GetUserDto>> UpdateAsync(Guid id, [FromBody] CreateOrEditUserDto model)
        {
            return Ok(await _service.UpdateAsync(id.ToString(), model));
        }
    }
}