using FluentValidation;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Validators;

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