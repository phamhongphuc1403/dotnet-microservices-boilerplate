using BuildingBlock.Application.CQRS;
using BuildingBlock.Domain.Shared.Services;
using TinyCRM.Identity.Application.Common.Services.Abstractions;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Application.DTOs.UserDTOs;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Handlers;

public class LoginCommandHandler : ICommandHandler<LoginCommand, LoginResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserOperationRepository _userOperationRepository;

    public LoginCommandHandler(IAuthService authService, ITokenService tokenService, IUnitOfWork unitOfWork,
        IUserOperationRepository userOperationRepository, IUserDomainService userDomainService)
    {
        _authService = authService;
        _tokenService = tokenService;
        _unitOfWork = unitOfWork;
        _userOperationRepository = userOperationRepository;
        _userDomainService = userDomainService;
    }

    public async Task<LoginResponseDto> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _authService.Login(request.Dto.Email, request.Dto.Password);

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