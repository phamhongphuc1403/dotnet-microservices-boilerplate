using Identitymanagement.Core.Domain.UserAggregate.Entities;

namespace Identitymanagement.Core.Domain.UserAggregate.Repositories;

public interface IUserOperationRepository
{
    Task UpdateAsync(User user);

    Task ChangePasswordAsync(User user, string currentPassword, string newPassword);

    Task CreateAsync(User user, string password);

    Task<bool> PasswordSignInAsync(User user, string password);

    Task ResetPasswordAsync(User user, string token, string newPassword);
}