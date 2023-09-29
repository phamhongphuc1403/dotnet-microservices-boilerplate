using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identities.Application.CQRS.Commands.Requests;
using TinyCRM.Identities.Application.DTOs;

namespace Identities.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto)
    {
        var response = await _mediator.Send(new LoginCommand(dto));

        return Ok(response);
    }
}