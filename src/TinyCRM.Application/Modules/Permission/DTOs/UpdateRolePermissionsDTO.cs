namespace TinyCRM.Application.Modules.Permission.DTOs;

public class UpdateRolePermissionsDto
{
    public List<string> PermissionTypes { get; set; } = new();
}