namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class CreateUserDto
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}