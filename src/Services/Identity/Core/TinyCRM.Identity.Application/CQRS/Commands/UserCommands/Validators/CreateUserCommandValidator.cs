using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Domain.Constants;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(320);

        RuleFor(x => x.Password)
            .NotEmpty()
            .Matches(Regex.Password)
            .WithMessage(
                "Password must has the minimum of eight characters, at least one uppercase letter and one number");
    }
}