using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Application.Modules.Permission.DTOs;
using TinyCRM.Application.Modules.Permission.Services;
using TinyCRM.Domain.Constants;
using TinyCRM.Domain.Entities;

namespace TinyCRM.API.Controllers;

[Route("api/permissions")]
[ApiController]
[Authorize(Roles = Role.SuperAdmin)]
public class PermissionController : ControllerBase
{
    private readonly IPermissionService _service;
    private readonly IPermissionCacheService _cacheService;

    public PermissionController(
        IPermissionService permissionService,
        IPermissionCacheService permissionCacheService
        )
    {
        _service = permissionService;
        _cacheService = permissionCacheService;
    }

    [HttpGet]
    public ActionResult<List<PermissionEntity>> GetAll()
    {
        return Ok(_service.GetAll());
    }

    [HttpGet("roles/{roleName}/permissions")]
    public async Task<ActionResult<IEnumerable<PermissionEntity>>> GetAllByRoleNameAsync(string roleName)
    {
        return Ok(await _service.GetAllByRoleNameAsync(roleName));
    }

    [HttpPut("roles/{roleName}/permissions")]
    public async Task<ActionResult> UpdateRolePermissionsAsync(string roleName,
        [FromBody] UpdateRolePermissionsDto model)
    {
        await _service.UpdateAsync(roleName, model);
        await _cacheService.RemoveByRoleNameAsync(roleName);
        return Ok();
    }
}