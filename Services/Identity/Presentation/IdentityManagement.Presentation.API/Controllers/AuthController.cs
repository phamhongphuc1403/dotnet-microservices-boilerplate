using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.Presentation.API.Controllers;

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
    public async Task<ActionResult<LoginResponseDto>> LoginAsync(LoginRequestDto dto)
    {
        var response = await _mediator.Send(new LoginCommand(dto));

        return Ok(response);
    }

    [HttpPost("register")]
    public async Task<ActionResult<UserSummaryDto>> RegisterAsync(CreateUserDto dto)
    {
        var user = await _mediator.Send(new RegisterCommand(dto));

        return Ok(user);
    }

    [HttpPost("refresh-token")]
    public async Task<ActionResult<TokenResponseDto>> GenerateRefreshTokenAsync(GenerateRefreshTokenRequestDto dto)
    {
        var response = await _mediator.Send(new GenerateRefreshTokenCommand(dto));

        return Ok(response);
    }
}