namespace IdentityManagement.Core.Application.Users.DTOs;

public class UserDetailDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? AvatarUrl { get; set; }

    public string? CoverUrl { get; set; }

    public DateTime CreatedAt { get; set; }

    public string CreatedBy { get; set; } = null!;

    public DateTime? UpdatedAt { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? DeletedAt { get; set; }

    public string? DeletedBy { get; set; }
}