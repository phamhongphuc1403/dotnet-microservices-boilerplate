using BuildingBlock.Application.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
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
    public async Task<ActionResult<FilterAndPagingResultDto<UserDto>>> GetAllUsers(
        [FromQuery] FilterAndPagingUsersDto dto)
    {
        var users = await _mediator.Send(new FilterAndPagingUsersQuery(dto));

        return Ok(users);
    }
}