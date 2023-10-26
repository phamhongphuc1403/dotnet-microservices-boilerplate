using BuildingBlock.Domain.Shared.Constants.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Requests;
using TinyCRM.Identity.Application.DTOs.RoleDTOs;

namespace Identity.API.Controllers;

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
    public async Task<ActionResult<RoleDto>> CreateAsync(CreateOrEditRoleDto dto)
    {
        var response = await _mediator.Send(new CreateRoleCommand(dto));

        return Ok(response);
    }
}