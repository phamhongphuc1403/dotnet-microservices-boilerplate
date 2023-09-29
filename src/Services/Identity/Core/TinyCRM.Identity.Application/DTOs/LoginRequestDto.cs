namespace TinyCRM.Identity.Application.DTOs;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}