using BuildingBlock.Core.Domain.Shared.Constants;
using IdentityManagement.Core.Application.Roles.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Roles.DTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.Presentation.API.Controllers;

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