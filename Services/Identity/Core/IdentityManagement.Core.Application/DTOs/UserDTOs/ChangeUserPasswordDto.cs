namespace IdentityManagement.Core.Application.DTOs.UserDTOs;

public class ChangeUserPasswordDto
{
    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}