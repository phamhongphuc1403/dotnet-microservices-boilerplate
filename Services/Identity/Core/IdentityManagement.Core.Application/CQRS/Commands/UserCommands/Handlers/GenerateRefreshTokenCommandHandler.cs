using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Application.Common.Services.Abstractions;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using IdentityManagement.Core.Application.DTOs.UserDTOs;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Handlers;

public class GenerateRefreshTokenCommandHandler : ICommandHandler<GenerateRefreshTokenCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserOperationRepository _userOperationRepository;

    public GenerateRefreshTokenCommandHandler(ITokenService tokenService, IAuthService authService,
        IUnitOfWork unitOfWork, IUserDomainService userDomainService, IUserOperationRepository userOperationRepository)
    {
        _tokenService = tokenService;
        _authService = authService;
        _unitOfWork = unitOfWork;
        _userDomainService = userDomainService;
        _userOperationRepository = userOperationRepository;
    }

    public async Task<LoginResponseDto> Handle(GenerateRefreshTokenCommand request, CancellationToken cancellationToken)
    {
        var user = await _tokenService.VerifyRefreshTokenAsync(request.Dto.RefreshToken);

        var claims = (await _authService.GetClaimsAsync(user)).ToList();

        var accessToken = _tokenService.GenerateAccessToken(claims);

        var refreshToken = _tokenService.GenerateRefreshToken(claims, user);

        _userDomainService.AddRefreshToken(user, refreshToken);

        await _userOperationRepository.UpdateAsync(user);

        await _unitOfWork.SaveChangesAsync();

        return new LoginResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}