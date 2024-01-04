namespace IdentityManagement.Core.Application.Users.DTOs;

public class UserDeletionDto
{
    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}