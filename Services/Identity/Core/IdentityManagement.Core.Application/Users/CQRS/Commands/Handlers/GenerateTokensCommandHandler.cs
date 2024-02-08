using BuildingBlock.Core.Application.CQRS;
using BuildingBlock.Core.Domain.Shared.Services;
using IdentityManagement.Core.Application.Shared.Services.Abstractions;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;
using IdentityManagement.Core.Application.Users.DTOs;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Handlers;

public class GenerateTokensCommandHandler : ICommandHandler<GenerateTokensCommand, TokenResponseDto>
{
    private readonly IAuthService _authService;
    private readonly ITokenService _tokenService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserDomainService _userDomainService;
    private readonly IUserOperationRepository _userOperationRepository;

    public GenerateTokensCommandHandler(IUnitOfWork unitOfWork, IUserDomainService userDomainService,
        IUserOperationRepository userOperationRepository, IAuthService authService, ITokenService tokenService)
    {
        _unitOfWork = unitOfWork;
        _userDomainService = userDomainService;
        _userOperationRepository = userOperationRepository;
        _authService = authService;
        _tokenService = tokenService;
    }

    public async Task<TokenResponseDto> Handle(GenerateTokensCommand request, CancellationToken cancellationToken)
    {
        var claims = (await _authService.GetClaimsAsync(request.User)).ToList();

        var accessToken = _tokenService.GenerateAccessToken(claims);

        var refreshToken = _tokenService.GenerateRefreshToken(claims, request.User);

        _userDomainService.AddRefreshToken(request.User, refreshToken);

        await _userOperationRepository.UpdateAsync(request.User);

        await _unitOfWork.SaveChangesAsync();

        return new TokenResponseDto
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken
        };
    }
}