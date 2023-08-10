using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Modules.Role.DTOs;
using TinyCRM.Application.Modules.Role.Services.Interfaces;
using TinyCRM.Domain.Constants;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Controllers
{
    [Route("api/roles")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService roleService)
        {
            _service = roleService;
        }

        [HttpGet]
        [Authorize(Policy = Permission.Role.View)]
        public async Task<ActionResult<List<RoleEntity>>> GetAllAsync()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("users/{userId:Guid}/role")]
        [Authorize(Policy = Permission.Role.View)]
        public async Task<ActionResult<RoleEntity>> GetUserRoleAsync(Guid userId)
        {
            return Ok(await _service.GetUserRolesAsync(userId.ToString()));
        }

        [HttpPut("users/{userId:Guid}/role")]
        [Authorize(Policy = Permission.Role.Update)]
        public async Task<IActionResult> UpdateAsync(Guid userId, [FromBody] UpdateUserRoleDto model)
        {
            await _service.UpdateUserRoleAsync(userId.ToString(), model);

            return Ok();
        }
    }
}