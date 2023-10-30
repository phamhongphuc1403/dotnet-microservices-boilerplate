using BuildingBlock.Application.CQRS;
using TinyCRM.Identity.Application.DTOs.UserDTOs;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

public class ChangeUserPasswordCommand : ChangeUserPasswordDto, ICommand
{
    public ChangeUserPasswordCommand(Guid id, ChangeUserPasswordDto dto) : base(dto)
    {
        UserId = id;
    }

    public Guid UserId { get; }
}