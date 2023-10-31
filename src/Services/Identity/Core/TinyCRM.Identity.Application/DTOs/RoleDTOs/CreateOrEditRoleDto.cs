namespace TinyCRM.Identity.Application.DTOs.RoleDTOs;

public class CreateOrEditRoleDto
{
    public CreateOrEditRoleDto()
    {
    }

    protected CreateOrEditRoleDto(CreateOrEditRoleDto dto)
    {
        Name = dto.Name;
    }

    public string Name { get; set; } = null!;
}