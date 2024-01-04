namespace IdentityManagement.Core.Application.Users.DTOs;

public class ChangeUserPasswordDto
{
    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}