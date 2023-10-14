using BuildingBlock.Domain.Constants.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.CQRS.Queries.RoleQueries.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace Identities.API.Controllers;

[ApiController]
[Route("api/roles")]
public class RoleController : ControllerBase
{
    private readonly IMediator _mediator;

    public RoleController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Authorize(Policy = Permissions.Role.Create)]
    public async Task<ActionResult<RoleDto>> Login(CreateOrEditRoleDto dto)
    {
        var response = await _mediator.Send(new CreateRoleCommand(dto));

        return Ok(response);
    }

    [HttpGet]
    [Authorize(Policy = Permissions.Role.View)]
    public async Task<ActionResult<IEnumerable<RoleDto>>> GetAllRoles()
    {
        var roles = await _mediator.Send(new GetAllRolesQuery());

        return Ok(roles);
    }
}