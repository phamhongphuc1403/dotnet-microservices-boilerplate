using BuildingBlock.Domain.Exceptions;
using BuildingBlock.Domain.Shared.Utils;
using TinyCRM.Identity.Domain.UserAggregate.DomainServices.Abstractions;
using TinyCRM.Identity.Domain.UserAggregate.Entities;
using TinyCRM.Identity.Domain.UserAggregate.Exceptions;
using TinyCRM.Identity.Domain.UserAggregate.Repositories;

namespace TinyCRM.Identity.Domain.UserAggregate.DomainServices.Implementations;

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

    public async Task<User> CreateAsync(string email, string password, string currentPassword)
    {
        await CheckValidOnCreate(email, password, currentPassword);

        var user = new User(email);

        return user;
    }

    private async Task CheckValidOnCreate(string email, string password, string currentPassword)
    {
        Optional<string>.Of(password).ThrowIfNotEqual(currentPassword,
            new ValidationException("Password and confirm password don't match"));

        await CheckIfEmailIsExisted(email);
    }

    private async Task CheckIfEmailIsExisted(string email)
    {
        Optional<bool>.Of(await _userReadOnlyRepository.CheckIfEmailExistAsync(email))
            .ThrowIfPresent(new UserConflictException(email));
    }
}