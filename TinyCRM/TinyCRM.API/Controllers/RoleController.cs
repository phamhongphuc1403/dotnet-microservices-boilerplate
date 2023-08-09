using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Modules.Account.DTOs;
using TinyCRM.Domain.Constants;

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
        public async Task<IActionResult> GetAllRolesAsync([FromQuery] RoleQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("users/{userId:Guid}/role")]
        [Authorize(Policy = Permission.Role.View)]
        public async Task<IActionResult> GetUserRoleAsync(Guid userId)
        {
            return Ok(await _service.GetUserRoleAsync(userId));
        }

        [HttpPut("users/{userId:Guid}/role")]
        [Authorize(Policy = Permission.Role.Update)]
        public async Task<IActionResult> UpdateUserRoleAsync(Guid userId, [FromBody] UpdateUserRoleDto model)
        {
            await _service.UpdateUserRoleAsync(userId, model);

            return Ok();
        }
    }

    public class UpdateUserRoleDto
    {
    }

    public class RoleQueryDto
    {
    }

    public interface IRoleService
    {
        Task<object?> GetAllAsync(RoleQueryDto query);
        Task<object?> GetUserRoleAsync(Guid userId);
        Task UpdateUserRoleAsync(Guid userId, UpdateUserRoleDto model);
    }
}
