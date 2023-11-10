using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Shared.Constants.Identity;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using IdentityManagement.Core.Application.CQRS.Queries.UserQueries.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/users")]
public class UserController : ControllerBase
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    [Authorize(Policy = Permissions.User.ViewAll)]
    public async Task<ActionResult<FilterAndPagingResultDto<UserDto>>> GetFilteredAndPagedUsersAsync(
        [FromQuery] FilterAndPagingUsersDto dto)
    {
        var users = await _mediator.Send(new FilterAndPagingUsersQuery(dto));

        return Ok(users);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.User.Create)]
    public async Task<ActionResult<UserDto>> CreateUserAsync(CreateUserDto dto)
    {
        var user = await _mediator.Send(new CreateUserCommand(dto));

        return Ok(user);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = Permissions.User.ViewAll)]
    public async Task<ActionResult<UserDto>> GetUserByIdAsync(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        return Ok(user);
    }

    [HttpGet("me")]
    [Authorize(Policy = Permissions.User.ViewPersonal)]
    public async Task<ActionResult<UserDto>> GetCurrentUserAsync()
    {
        var user = await _mediator.Send(new GetCurrentUserQuery());

        return Ok(user);
    }

    [HttpPut("me/change-password")]
    [Authorize(Policy = Permissions.User.EditPersonal)]
    public async Task<ActionResult> ChangeCurrentPasswordAsync(ChangeCurrentPasswordDto dto)
    {
        await _mediator.Send(new ChangeCurrentPasswordCommand(dto));

        return NoContent();
    }

    [HttpPut("{id:guid}/change-password")]
    [Authorize(Policy = Permissions.User.EditAll)]
    public async Task<ActionResult> ChangeUserPasswordAsync(Guid id, ChangeUserPasswordDto dto)
    {
        await _mediator.Send(new ChangeUserPasswordCommand(id, dto));

        return NoContent();
    }
}