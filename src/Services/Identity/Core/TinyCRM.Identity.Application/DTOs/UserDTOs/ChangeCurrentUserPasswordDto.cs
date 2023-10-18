namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class ChangeCurrentUserPasswordDto
{
    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}