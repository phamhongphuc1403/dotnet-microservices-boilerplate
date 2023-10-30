using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

public class ChangeCurrentPasswordCommand : ChangeCurrentPasswordDto, ICommand
{
    public ChangeCurrentPasswordCommand(ChangeCurrentPasswordDto dto)
    {
        CurrentPassword = dto.CurrentPassword;
        NewPassword = dto.NewPassword;
        ConfirmPassword = dto.ConfirmPassword;
    }
}