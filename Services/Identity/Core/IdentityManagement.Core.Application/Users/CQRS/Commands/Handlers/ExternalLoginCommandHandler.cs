using BuildingBlock.Core.Application.CQRS;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Specifications;
using MediatR;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class ExternalLoginCommandHandler : ICommandHandler<ExternalLoginCommand, LoginResponseDto>
{
    private readonly IMediator _mediator;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public ExternalLoginCommandHandler(IUserReadOnlyRepository userReadOnlyRepository, IMediator mediator)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _mediator = mediator;
    }

    public async Task<LoginResponseDto> Handle(ExternalLoginCommand request, CancellationToken cancellationToken)
    {
        var userInfo = await request.ExternalLoginService.ValidateAsync(request.Token);

        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(userInfo.Email);

        var existingUser = await _userReadOnlyRepository.GetAnyAsync(userEmailExactMatchSpecification);

        if (existingUser is null) existingUser = await _mediator.Send(new CreateUserCommand(userInfo));

        var tokenResponseDto = await _mediator.Send(new GenerateTokensCommand(existingUser), cancellationToken);

        return new LoginResponseDto
        {
            Token = tokenResponseDto,
            EmailConfirmed = existingUser.EmailConfirmed
        };
    }
}