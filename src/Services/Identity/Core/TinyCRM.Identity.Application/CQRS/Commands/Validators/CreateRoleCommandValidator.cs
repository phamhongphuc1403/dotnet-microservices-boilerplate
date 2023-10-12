using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;

namespace TinyCRM.Identity.Application.CQRS.Commands.Validators;

public class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(role => role.Name)
            .NotEmpty()
            .MaximumLength(256);
    }
}