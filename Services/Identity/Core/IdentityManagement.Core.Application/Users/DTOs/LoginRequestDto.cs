namespace IdentityManagement.Core.Application.Users.DTOs;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}