using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(user => user.Email)
            .NotEmpty()
            .When(user => string.IsNullOrWhiteSpace(user.Email))
            ;

        RuleFor(user => user.Password)
            .NotEmpty()
            .When(user => string.IsNullOrWhiteSpace(user.Password))
            ;
    }
}