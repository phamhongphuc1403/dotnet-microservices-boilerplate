using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
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
    public async Task<ActionResult<RoleDto>> Login(CreateOrEditRoleDto dto)
    {
        var response = await _mediator.Send(new CreateRoleCommand(dto));

        return Ok(response);
    }
}