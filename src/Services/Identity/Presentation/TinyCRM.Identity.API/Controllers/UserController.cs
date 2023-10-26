using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Shared.Constants.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace Identity.API.Controllers;

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
    public async Task<ActionResult<FilterAndPagingResultDto<UserDto>>> GetAllUsers(
        [FromQuery] FilterAndPagingUsersDto dto)
    {
        var users = await _mediator.Send(new FilterAndPagingUsersQuery(dto));

        return Ok(users);
    }

    [HttpPost]
    [Authorize(Policy = Permissions.User.Create)]
    public async Task<ActionResult<UserDto>> CreateUser(CreateUserDto dto)
    {
        var user = await _mediator.Send(new CreateUserCommand(dto));

        return Ok(user);
    }

    [HttpGet("{id:guid}")]
    [Authorize(Policy = Permissions.User.ViewAll)]
    public async Task<ActionResult<UserDto>> GetUserById(Guid id)
    {
        var user = await _mediator.Send(new GetUserByIdQuery(id));

        return Ok(user);
    }

    [HttpGet("me")]
    [Authorize(Policy = Permissions.User.ViewPersonal)]
    public async Task<ActionResult<UserDto>> GetCurrentUser()
    {
        var user = await _mediator.Send(new GetCurrentUserQuery());

        return Ok(user);
    }

    [HttpPut("me/change-password")]
    [Authorize(Policy = Permissions.User.EditPersonal)]
    public async Task<ActionResult> ChangeUserPassword(ChangeCurrentUserPasswordDto dto)
    {
        await _mediator.Send(new ChangeCurrentPasswordCommand(dto));

        return NoContent();
    }
}