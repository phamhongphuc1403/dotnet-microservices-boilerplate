namespace TinyCRM.Identity.Application.DTOs.UserDTOs;

public class ChangeCurrentPasswordDto
{
    public ChangeCurrentPasswordDto()
    {
    }

    protected ChangeCurrentPasswordDto(ChangeCurrentPasswordDto dto)
    {
        CurrentPassword = dto.CurrentPassword;
        NewPassword = dto.NewPassword;
        ConfirmPassword = dto.ConfirmPassword;
    }

    public string CurrentPassword { get; set; } = null!;
    public string NewPassword { get; set; } = null!;
    public string ConfirmPassword { get; set; } = null!;
}