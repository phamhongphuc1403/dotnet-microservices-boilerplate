using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

public class ChangeCurrentPasswordCommand : ChangeCurrentUserPasswordDto, ICommand
{
    public ChangeCurrentPasswordCommand(ChangeCurrentUserPasswordDto dto)
    {
        CurrentPassword = dto.CurrentPassword;
        NewPassword = dto.NewPassword;
        ConfirmPassword = dto.ConfirmPassword;
    }
}