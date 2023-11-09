using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Handlers;

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