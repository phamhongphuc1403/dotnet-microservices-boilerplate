namespace IdentityManagement.Core.Application.Users.DTOs;

public class ChangeCurrentPasswordDto
{
    public string CurrentPassword { get; set; } = null!;

    public string NewPassword { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}