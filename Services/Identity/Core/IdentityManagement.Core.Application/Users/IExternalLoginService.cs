using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Application.Users;

public interface IExternalLoginService
{
    public Task<User> ValidateAsync(string token);
}