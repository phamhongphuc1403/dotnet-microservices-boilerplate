using TinyCRM.Identity.Domain.UserAggregate.Entities;

namespace TinyCRM.Identity.Domain.UserAggregate.Repositories;

public interface IUserOperationRepository
{
    Task UpdateAsync(User user);

    Task ChangePasswordAsync(User user, string currentPassword, string newPassword);

    Task CreateAsync(User user, string password);

    Task<bool> PasswordSignInAsync(User user, string password);
}