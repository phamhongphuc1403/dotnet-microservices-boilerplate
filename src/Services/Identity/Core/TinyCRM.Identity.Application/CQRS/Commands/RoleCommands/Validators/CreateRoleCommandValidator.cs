using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Requests;

namespace TinyCRM.Identity.Application.CQRS.Commands.RoleCommands.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}