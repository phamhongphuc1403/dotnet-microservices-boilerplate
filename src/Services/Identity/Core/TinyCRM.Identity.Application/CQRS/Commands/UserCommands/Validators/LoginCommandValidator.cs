using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(user => user.Dto.Email)
            .NotEmpty()
            .When(user => string.IsNullOrWhiteSpace(user.Dto.Email))
            ;

        RuleFor(user => user.Dto.Password)
            .NotEmpty()
            .When(user => string.IsNullOrWhiteSpace(user.Dto.Password))
            ;
    }
}