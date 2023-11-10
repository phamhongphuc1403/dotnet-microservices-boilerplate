using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
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

    [HttpPost("refresh-token")]
    public async Task<ActionResult<LoginResponseDto>> GenerateRefreshTokenAsync(GenerateRefreshTokenRequestDto dto)
    {
        var response = await _mediator.Send(new GenerateRefreshTokenCommand(dto));

        return Ok(response);
    }
}