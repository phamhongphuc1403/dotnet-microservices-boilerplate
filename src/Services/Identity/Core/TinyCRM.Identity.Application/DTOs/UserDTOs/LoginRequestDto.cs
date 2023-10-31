namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class LoginRequestDto
{
    public LoginRequestDto()
    {
    }

    protected LoginRequestDto(LoginRequestDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
    }

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;
}