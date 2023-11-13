using System.Security.Claims;
using BuildingBlock.Core.Domain.Exceptions;
using BuildingBlock.Core.Domain.Shared.Utils;
using Identitymanagement.Core.Domain.RoleAggregate.Repositories;
using Identitymanagement.Core.Domain.UserAggregate.DomainServices.Abstractions;
using Identitymanagement.Core.Domain.UserAggregate.Entities;
using Identitymanagement.Core.Domain.UserAggregate.Repositories;

namespace Identitymanagement.Core.Domain.UserAggregate.DomainServices.Implementations;

public class AuthService : IAuthService
{
    private readonly IRoleReadOnlyRepository _roleReadOnlyRepository;
    private readonly IUserOperationRepository _userOperationRepository;
    private readonly IUserReadOnlyRepository _userReadOnlyRepository;

    public AuthService(IUserOperationRepository userOperationRepository,
        IUserReadOnlyRepository userReadOnlyRepository, IRoleReadOnlyRepository roleReadOnlyRepository)
    {
        _userOperationRepository = userOperationRepository;
        _userReadOnlyRepository = userReadOnlyRepository;
        _roleReadOnlyRepository = roleReadOnlyRepository;
    }

    public async Task<User> Login(string email, string password)
    {
        var user = await _userReadOnlyRepository.GetByEmailAsync(email);

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

        var roles = await _roleReadOnlyRepository.GetNameByUserIdAsync(user.Id);

        claims.AddRange(roles.Select(role => new Claim(ClaimTypes.Role, role)));

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