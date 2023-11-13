using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Application.Common.Services.Abstractions;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using Identitymanagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.UserAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Exceptions;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Handlers;

public class ChangeUserPasswordCommandHandler : ICommandHandler<ChangeUserPasswordCommand>
{
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public ChangeUserPasswordCommandHandler(IUserReadOnlyRepository userReadOnlyRepository,
        IUserOperationRepository userOperationRepository, ITokenService tokenService, IUnitOfWork unitOfWork,
        IUserDomainService userDomainService)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _userOperationRepository = userOperationRepository;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userDomainService = userDomainService;
    }

    public async Task Handle(ChangeUserPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = Optional<User>.Of(await _userReadOnlyRepository.GetByIdAsync(request.UserId))
            .ThrowIfNotExist(new UserNotFoundException(request.UserId)).Get();

        var token = await _userDomainService.ResetPasswordAsync(user, request.Dto.Password,
            request.Dto.ConfirmPassword);

        await _userOperationRepository.ResetPasswordAsync(user, token, request.Dto.Password);

        await _tokenService.RevokeAllRefreshTokensAsync(user);

        await _userOperationRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }
}