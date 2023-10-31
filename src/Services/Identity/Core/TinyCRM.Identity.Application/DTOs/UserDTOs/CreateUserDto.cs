namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class CreateUserDto
{
    public CreateUserDto()
    {
    }

    protected CreateUserDto(CreateUserDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
        ConfirmPassword = dto.ConfirmPassword;
    }

    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}