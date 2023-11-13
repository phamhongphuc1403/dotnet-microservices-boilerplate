using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Shared.Utils;
using Identitymanagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.UserAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Exceptions;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;

namespace Identitymanagement.Core.Domain.UserAggregate.DomainServices.Implementations;

public class UserDomainService : IUserDomainService
{
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public UserDomainService(IUserReadOnlyRepository userReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
    }

    public void AddRefreshToken(User user, string refreshToken)
    {
        user.RefreshTokens.Add(new RefreshToken(refreshToken));
    }

    public async Task<User> CreateAsync(string email, string password, string confirmPassword)
    {
        await CheckValidOnCreate(email, password, confirmPassword);

        var user = new User(email);

        return user;
    }

    public async Task<string> ResetPasswordAsync(User user, string password, string confirmPassword)
    {
        Optional<string>.Of(password).ThrowIfNotEqual(confirmPassword,
            new ValidationException("Password and confirm password don't match"));

        return await _userReadOnlyRepository.GetPasswordResetToken(user);
    }

    private async Task CheckValidOnCreate(string email, string password, string confirmPassword)
    {
        Optional<string>.Of(password).ThrowIfNotEqual(confirmPassword,
            new ValidationException("Password and confirm password don't match"));

        await CheckIfEmailIsExisted(email);
    }

    private async Task CheckIfEmailIsExisted(string email)
    {
        Optional<bool>.Of(await _userReadOnlyRepository.CheckIfEmailExistAsync(email))
            .ThrowIfExist(new UserConflictException(email));
    }
}