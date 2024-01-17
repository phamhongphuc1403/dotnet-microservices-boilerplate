using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Specifications;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Exceptions;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Specifications;

namespace IdentityManagement.Core.Domain.UserAggregate.DomainServices.Implementations;

public class UserDomainService : IUserDomainService
{
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public UserDomainService(IUserReadOnlyRepository userReadOnlyRepository,
        IRoleReadOnlyRepository roleReadOnlyRepository)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
    }

    public void AddRefreshToken(User user, string refreshToken)
    {
        user.RefreshTokens.Add(new RefreshToken(refreshToken));
    }

    public async Task<User> CreateAsync(string email, string name, string password,
        string confirmPassword)
    {
        await CheckValidOnCreate(email);

        var user = new User(email, name);

        return user;
    }

    public async Task DeleteAsync(User user)
    {
        await CheckValidOnDeleteAsync(user);
    }

    private async Task CheckValidOnDeleteAsync(User user)
    {
        var roleIdSpecification = new RoleUserIdSpecification(user.Id);

        var roleNameExactMatchSpecification = new RoleNameExactMatchSpecification("admin");

        var specification = roleIdSpecification.And(roleNameExactMatchSpecification);

        var role = await _roleReadOnlyRepository.CheckIfExistAsync(specification);

        if (role) throw new ValidationException("User with admin role can not be deleted");
    }

    private async Task CheckValidOnCreate(string email)
    {
        // await ThrowIfPhoneNumberIsExistAsync(phoneNumber);

        await ThrowIfEmailIsExistAsync(email);
    }

    private async Task ThrowIfPhoneNumberIsExistAsync(string phoneNumber)
    {
        var userPhoneNumberExactMatchSpecification = new UserPhoneNumberExactMatchSpecification(phoneNumber);

        Optional<bool>.Of(await _userReadOnlyRepository.CheckIfExistAsync(userPhoneNumberExactMatchSpecification))
            .ThrowIfExist(new UserConflictException("phone number", phoneNumber));
    }

    private async Task ThrowIfEmailIsExistAsync(string email)
    {
        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(email);

        Optional<bool>.Of(await _userReadOnlyRepository.CheckIfExistAsync(userEmailExactMatchSpecification))
            .ThrowIfExist(new UserConflictException("email", email));
    }
}