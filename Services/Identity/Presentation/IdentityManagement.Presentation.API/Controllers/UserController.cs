using BuildingBlock.Core.Application.DTOs;
using BuildingBlock.Core.Domain.Shared.Constants;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.CQRS.Queries.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
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
    public async Task<ActionResult<FilterAndPagingResultDto<UserDetailDto>>> GetFilteredAndPagedUsersAsync(
        [FromQuery] FilterAndPagingUsersDto dto)
    {
        var users = await _mediator.Send(new FilterAndPagingUsersQuery(dto));

        return Ok(users);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.User.Create)]
    public async Task<ActionResult<UserDetailDto>> CreateUserAsync(CreateUserDto dto)
    {
        var user = await _mediator.Send(new CreateUserCommand(dto));

        return Ok(user);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = Permissions.User.ViewAll)]
    public async Task<ActionResult<UserDetailDto>> GetUserByIdAsync(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        return Ok(user);
    }

    [HttpGet("me")]
    [Authorize(Policy = Permissions.User.ViewPersonal)]
    public async Task<ActionResult<UserSummaryDto>> GetCurrentUserAsync()
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

    [HttpDelete("{userId:guid}")]
    [Authorize(Policy = Permissions.User.DeleteAll)]
    public async Task<ActionResult> DeleteUserAsync(Guid userId)
    {
        await _mediator.Send(new DeleteUserCommand(userId));

        return NoContent();
    }
}