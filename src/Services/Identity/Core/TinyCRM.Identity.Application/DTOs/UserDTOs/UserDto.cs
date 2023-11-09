namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class UserDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}