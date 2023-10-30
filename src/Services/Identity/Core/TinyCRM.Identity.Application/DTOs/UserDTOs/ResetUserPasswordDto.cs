namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class ResetUserPasswordDto
{
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}