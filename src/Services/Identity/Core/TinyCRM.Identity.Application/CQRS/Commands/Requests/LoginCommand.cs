using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.Requests;

public class LoginCommand : LoginRequestDto, ICommand<LoginResponseDto>
{
    public LoginCommand(LoginRequestDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
    }
}