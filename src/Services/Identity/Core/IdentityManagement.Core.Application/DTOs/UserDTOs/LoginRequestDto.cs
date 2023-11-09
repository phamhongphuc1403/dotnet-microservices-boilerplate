namespace IdentityManagement.Core.Application.DTOs.UserDTOs;

public class LoginRequestDto
{
    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}