using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.Requests;

namespace TinyCRM.Identity.Application.CQRS.Commands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .When(user => string.IsNullOrWhiteSpace(user.Email))
            // .EmailAddress()
            // .WithMessage("'{PropertyValue}' is not a valid email address.")
            ;

        RuleFor(user => user.Password)
            .NotEmpty()
            .When(user => string.IsNullOrWhiteSpace(user.Password))
            // .MinimumLength(6)
            // .WithMessage("'{PropertyName}' must be at least {MinLength} characters long.")
            ;
    }
}