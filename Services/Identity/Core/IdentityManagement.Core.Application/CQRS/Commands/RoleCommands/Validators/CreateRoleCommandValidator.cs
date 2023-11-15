using FluentValidation;
using IdentityManagement.Core.Application.CQRS.Commands.RoleCommands.Requests;

namespace IdentityManagement.Core.Application.CQRS.Commands.RoleCommands.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Dto.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}