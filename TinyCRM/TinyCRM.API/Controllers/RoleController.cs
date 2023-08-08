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
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetAllRolesAsync([FromQuery] RoleQueryDto query)
        {
            return Ok(await _service.GetAllAsync(query));
        }

        [HttpGet("users/{userId:Guid}/roles")]
        [Authorize(Roles = Role.Admin)]
        public async Task<IActionResult> GetUserRoleAsync(Guid userId)
        {
            return Ok(await _service.GetUserRoleAsync(userId));
        }
    }

    public class RoleQueryDto
    {
    }

    public interface IRoleService
    {
        Task<object?> GetAllAsync(RoleQueryDto query);
        Task<object?> GetUserRoleAsync(Guid userId);
    }
}
