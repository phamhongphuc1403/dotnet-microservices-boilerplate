using AutoMapper;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Infrastructure.Google;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace IdentityManagement.Presentation.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{
    private readonly IConfiguration _configuration;
    private readonly IMapper _mapper;
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator, IMapper mapper, IConfiguration configuration)
    {
        _mediator = mediator;
        _mapper = mapper;
        _configuration = configuration;
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

    [HttpPost("external-login")]
    public async Task<ActionResult<LoginResponseDto>> ExternalLoginAsync([FromQuery] ExternalLoginDto dto)
    {
        var externalLoginService = ExternalLoginProvider.GetLoginProvider(dto.AuthProvider, _mapper, _configuration);

        var response = await _mediator.Send(new ExternalLoginCommand(dto.Token, externalLoginService));

        return Ok(response);
    }
}