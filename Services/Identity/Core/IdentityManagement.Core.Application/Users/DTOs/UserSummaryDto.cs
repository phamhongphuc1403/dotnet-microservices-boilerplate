namespace IdentityManagement.Core.Application.Users.DTOs;

public class UserSummaryDto
{
    public Guid Id { get; set; }

    public string Email { get; set; } = null!;

    public string Name { get; set; } = null!;

    public string PhoneNumber { get; set; } = null!;

    public bool EmailConfirmed { get; set; }

    public bool PhoneNumberConfirmed { get; set; }

    public string? AvatarUrl { get; set; }

    public string? CoverUrl { get; set; }
}