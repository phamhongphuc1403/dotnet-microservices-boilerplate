using BuildingBlock.Application;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Exceptions;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Handlers;

public class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeCurrentUserPasswordCommand>
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;

    public ChangeUserPasswordCommandHandler(IAuthService authService, ICurrentUser currentUser)
    {
        _authService = authService;
        _currentUser = currentUser;
    }

    public async Task Handle(ChangeCurrentUserPasswordCommand request, CancellationToken cancellationToken)
    {
        if (request.NewPassword != request.ConfirmPassword)
            throw new ValidationException("Password and confirmation password do not match");

        await _authService.ChangePasswordAsync(_currentUser.Id, request.CurrentPassword, request.NewPassword);
    }
}