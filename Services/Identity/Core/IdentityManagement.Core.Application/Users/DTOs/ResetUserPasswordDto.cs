namespace IdentityManagement.Core.Application.Users.DTOs;

public class ResetUserPasswordDto
{
    public string NewPassword { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}