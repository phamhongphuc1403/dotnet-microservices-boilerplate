using MediatR;
using Microsoft.AspNetCore.Mvc;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;
using TinyCRM.Identity.Application.DTOs;

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

    [HttpPost("login")]
    public async Task<ActionResult<LoginResponseDto>> Login(LoginRequestDto dto)
    {
        var response = await _mediator.Send(new LoginCommand(dto));

        return Ok(response);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginResponseDto>> Login(GenerateRefreshTokenRequestDto dto)
    {
        var response = await _mediator.Send(new GenerateRefreshTokenCommand(dto));

        return Ok(response);
    }
}