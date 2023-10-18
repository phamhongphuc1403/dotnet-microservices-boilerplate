namespace TinyCRM.Identity.Domain.PermissionAggregate.DomainServices;

public interface IPermissionDomainService
{
    Task<IEnumerable<string>> GetPermissionsAsync(string roleName);
}