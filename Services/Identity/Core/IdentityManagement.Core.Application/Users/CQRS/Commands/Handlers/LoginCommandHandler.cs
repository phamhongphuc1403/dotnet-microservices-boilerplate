using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Application.Shared.Services.Abstractions;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

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
            Token = new TokenResponseDto
            {
                AccessToken = accessToken,
                RefreshToken = refreshToken
            },
            EmailConfirmed = user.EmailConfirmed
        };
    }
}