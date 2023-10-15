namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class UserDto
{
    public Guid Id { get; set; }
    public string Email { get; set; } = null!;
    public string CreatedAt { get; set; } = null!;
    public string CreatedBy { get; set; } = null!;
    public string? UpdatedAt { get; set; }
    public string? UpdatedBy { get; set; }
    public string? DeletedAt { get; set; }
    public string? DeletedBy { get; set; }
}