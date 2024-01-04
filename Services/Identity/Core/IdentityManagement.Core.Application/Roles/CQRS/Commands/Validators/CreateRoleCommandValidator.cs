using FluentValidation;
using IdentityManagement.Core.Application.Roles.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Roles.CQRS.Commands.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Dto.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}