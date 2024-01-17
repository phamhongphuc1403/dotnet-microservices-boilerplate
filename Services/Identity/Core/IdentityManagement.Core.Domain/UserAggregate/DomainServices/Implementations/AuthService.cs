using System.Security.Claims;
using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Shared.Utils;
using IdentityManagement.Core.Domain.RoleAggregate.Exceptions;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Specifications;

namespace IdentityManagement.Core.Domain.UserAggregate.DomainServices.Implementations;

public class AuthService : IAuthService
{
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;


    public AuthService(IUserOperationRepository userOperationRepository, IUserReadOnlyRepository userReadOnlyRepository,
        IRoleReadOnlyRepository roleReadOnlyRepository)
    {
        _userOperationRepository = userOperationRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
    }

    public async Task<User> Login(string email, string password)
    {
        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(email);

        var user = await _userReadOnlyRepository.GetAnyAsync(userEmailExactMatchSpecification);

        if (user == null) throw new ValidationException("Invalid email or password");

        var result = await _userOperationRepository.PasswordSignInAsync(user, password);

        if (result) return user;

        throw new ValidationException("Invalid email or password");
    }

    public async Task<IEnumerable<Claim>> GetClaimsAsync(User user)
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.NameIdentifier, user.Id.ToString()),
            new(ClaimTypes.Email, user.Email)
        };

        var roleUserIdSpecification = new RoleUserIdSpecification(user.Id);

        var roles = await _roleReadOnlyRepository.GetAllAsync(roleUserIdSpecification);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role.Name)));

        return claims;
    }

    public async Task ChangePasswordAsync(User user, string currentPassword, string newPassword,
        string confirmPassword)
    {
        await CheckValidOnChangePassword(user, currentPassword, newPassword, confirmPassword);

        await _userOperationRepository.ChangePasswordAsync(user, currentPassword, newPassword);
    }

    private async Task CheckValidOnChangePassword(User user, string currentPassword,
        string newPassword, string confirmPassword)
    {
        Optional<string>.Of(newPassword).ThrowIfNotEqual(confirmPassword,
            new ValidationException("Password and confirmation password do not match"));

        Optional<bool>.Of(await _userReadOnlyRepository.CheckPasswordAsync(user, currentPassword))
            .ThrowIfNotExist(new ValidationException("Invalid password"));

        if (currentPassword == newPassword)
            throw new ValidationException("New password must be different from current password");
    }
}