using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

public class LoginCommand : LoginRequestDto, ICommand<LoginResponseDto>
{
    public LoginCommand(LoginRequestDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
    }
}