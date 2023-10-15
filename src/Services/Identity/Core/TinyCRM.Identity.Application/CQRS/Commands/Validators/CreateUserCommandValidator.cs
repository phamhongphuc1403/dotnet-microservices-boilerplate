using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;

namespace TinyCRM.Identity.Application.CQRS.Commands.Validators;

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
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage(
                "Password must has the minimum of eight characters, at least one uppercase letter and one number");
    }
}