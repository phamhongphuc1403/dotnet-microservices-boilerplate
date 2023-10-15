using BuildingBlock.Application.DTOs;
using BuildingBlock.Domain.Constants.Identity;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.CQRS.Queries.UserQueries.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace Identities.API.Controllers;

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
}