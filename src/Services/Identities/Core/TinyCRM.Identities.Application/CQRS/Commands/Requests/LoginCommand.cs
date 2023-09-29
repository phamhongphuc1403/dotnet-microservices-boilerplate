using BuildingBlock.Application.CQRS;
using TinyCRM.Identities.Application.DTOs;

namespace TinyCRM.Identities.Application.CQRS.Commands.Requests;

public class LoginCommand : LoginRequestDto, ICommand<LoginResponseDto>
{
    public LoginCommand(LoginRequestDto dto)
    {
        Email = dto.Email;
        Password = dto.Password;
    }
}