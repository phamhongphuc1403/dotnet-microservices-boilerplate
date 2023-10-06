namespace TinyCRM.Identity.Application.Services.Abstractions;

public interface IPermissionService
{
    Task<IEnumerable<string>> GetPermissionsAsync(string roleName);
}