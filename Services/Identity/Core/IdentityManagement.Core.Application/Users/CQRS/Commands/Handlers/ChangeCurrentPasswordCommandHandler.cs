using BuildingBlock.Core.Application;
using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using BuildingBlock.Core.Domain.Specifications.Implementations;
using IdentityManagement.Core.Application.Shared.Services.Abstractions;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

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
        var userIdSpecification = new EntityIdSpecification<User>(_currentUser.Id);

        var user = Optional<User>.Of(await _userReadOnlyRepository.GetAnyAsync(userIdSpecification))
            .ThrowIfNotExist(new UserNotFoundException(_currentUser.Id)).Get();

        await _authService.ChangePasswordAsync(user, request.Dto.CurrentPassword, request.Dto.NewPassword);

        await _unitOfWork.SaveChangesAsync();

        await _tokenService.RevokeAllRefreshTokensAsync(user);

        await _userOperationRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }
}