using BuildingBlock.Application;
using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Shared.Services;
using BuildingBlock.Domain.Shared.Utils;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Handlers;

public class ChangeCurrentPasswordCommandHandler : ICommandHandler<ChangeCurrentPasswordCommand>
{
    private readonly IAuthService _authService;
    private readonly ICurrentUser _currentUser;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public ChangeCurrentPasswordCommandHandler(IAuthService authService, ICurrentUser currentUser,
        IUserReadOnlyRepository userReadOnlyRepository, ITokenService tokenService, IUnitOfWork unitOfWork,
        IUserOperationRepository userOperationRepository)
    {
        _authService = authService;
        _currentUser = currentUser;
        _userReadOnlyRepository = userReadOnlyRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userOperationRepository = userOperationRepository;
    }

    public async Task Handle(ChangeCurrentPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = Optional<User>.Of(await _userReadOnlyRepository.GetByIdAsync(_currentUser.Id))
            .ThrowIfNotPresent(new UserNotFoundException(_currentUser.Id)).Get();

        await _authService.ChangePasswordAsync(user, request.CurrentPassword, request.NewPassword,
            request.ConfirmPassword);

        await _unitOfWork.SaveChangesAsync();

        await _tokenService.RevokeAllRefreshTokensAsync(user);

        await _userOperationRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }
}