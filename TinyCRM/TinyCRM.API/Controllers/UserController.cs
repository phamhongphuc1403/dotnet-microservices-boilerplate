using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.API.Constants;
using TinyCRM.API.Modules.Account.DTOs;
using TinyCRM.API.Modules.User.DTOs;
using TinyCRM.API.Modules.User.Services;

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
        public async Task<ActionResult<GetUserDTO>> CreateAsync([FromBody] CreateOrEditUserDTO model)
        {
            var newUser = await _service.CreateAsync(model);
            return CreatedAtAction(nameof(GetByIdAsync), new { id = newUser.Id }, newUser);
        }

        [HttpGet("{id:guid}")]
        [Authorize(Policy = "ViewAndUpdateUserPermission")]
        [ActionName(nameof(GetByIdAsync))]
        public async Task<ActionResult<GetAccountDTO>> GetByIdAsync(Guid id)
        {
            return Ok(await _service.GetByIdAsync(id));
        }

        [HttpPut("{id:guid}")]
        [Authorize(Policy = "ViewAndUpdateUserPermission")]
        public async Task<ActionResult<GetUserDTO>> UpdateAsync(Guid id, [FromBody] CreateOrEditUserDTO model)
        {

            return Ok(await _service.UpdateAsync(id, model));
        }
    }
}