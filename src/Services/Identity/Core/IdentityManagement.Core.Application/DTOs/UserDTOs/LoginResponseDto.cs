namespace IdentityManagement.Core.Application.DTOs.UserDTOs;

public class LoginResponseDto
{
    public string AccessToken { get; set; } = null!;

    public string RefreshToken { get; set; } = null!;
}