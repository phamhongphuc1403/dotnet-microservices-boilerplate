using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

public class CreateUserCommand : CreateUserDto, ICommand<UserDto>
{
    public CreateUserCommand(CreateUserDto dto) : base(dto)
    {
    }
}