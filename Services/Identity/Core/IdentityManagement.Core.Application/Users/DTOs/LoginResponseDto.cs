namespace IdentityManagement.Core.Application.Users.DTOs;

public class LoginResponseDto
{
    public TokenResponseDto Token { get; set; } = null!;

    public bool EmailConfirmed { get; set; }
}