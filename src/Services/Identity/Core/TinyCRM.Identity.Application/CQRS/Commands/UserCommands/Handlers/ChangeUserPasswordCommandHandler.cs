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
            .ThrowIfNotPresent(new UserNotFoundException(request.UserId)).Get();

        var token = await _userDomainService.ResetPasswordAsync(user, request.Password, request.ConfirmPassword);

        await _userOperationRepository.ResetPasswordAsync(user, token, request.Password);

        await _tokenService.RevokeAllRefreshTokensAsync(user);

        await _userOperationRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }
}