using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using MediatR;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly IMediator _mediator;

    public LoginCommandHandler(IAuthService authService, IMediator mediator)
    {
        _authService = authService;
        _mediator = mediator;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.Login(request.Dto.Email, request.Dto.Password);

        var tokenResponseDto = await _mediator.Send(new GenerateTokensCommand(user), cancellationToken);

        return new LoginResponseDto
        {
            Token = tokenResponseDto,
            EmailConfirmed = user.EmailConfirmed
        };
    }
}