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
        var userIdSpecification = new EntityIdSpecification<User>(request.UserId);

        var user = Optional<User>.Of(await _userReadOnlyRepository.GetAnyAsync(userIdSpecification))
            .ThrowIfNotExist(new UserNotFoundException(request.UserId)).Get();

        var token = await _userDomainService.ResetPasswordAsync(user, request.Dto.Password,
            request.Dto.ConfirmPassword);

        await _userOperationRepository.ResetPasswordAsync(user, token, request.Dto.Password);

        await _unitOfWork.SaveChangesAsync();

        await _tokenService.RevokeAllRefreshTokensAsync(user);

        await _userOperationRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();
    }
}