namespace TinyCRM.Identity.Domain.PermissionAggregate.Repositories;

public interface IPermissionReadOnlyRepository
{
    Task<IEnumerable<string>> GetNamesByRoleNameAsync(string roleName);
}