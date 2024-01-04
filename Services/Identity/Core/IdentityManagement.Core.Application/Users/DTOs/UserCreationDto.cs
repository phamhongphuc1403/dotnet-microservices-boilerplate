namespace IdentityManagement.Core.Application.Users.DTOs;

public class UserCreationDto
{
    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;
}