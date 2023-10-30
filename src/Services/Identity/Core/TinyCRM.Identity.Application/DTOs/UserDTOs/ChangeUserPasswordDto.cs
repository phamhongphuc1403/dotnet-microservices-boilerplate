namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class ChangeUserPasswordDto
{
    protected ChangeUserPasswordDto(ChangeUserPasswordDto dto)
    {
        Password = dto.Password;
        ConfirmPassword = dto.ConfirmPassword;
    }

    public ChangeUserPasswordDto()
    {
    }

    public string Password { get; set; } = null!;

    public string ConfirmPassword { get; set; } = null!;
}