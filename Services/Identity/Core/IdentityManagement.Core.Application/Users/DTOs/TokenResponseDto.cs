namespace IdentityManagement.Core.Application.Users.DTOs;

public class TokenResponseDto
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}