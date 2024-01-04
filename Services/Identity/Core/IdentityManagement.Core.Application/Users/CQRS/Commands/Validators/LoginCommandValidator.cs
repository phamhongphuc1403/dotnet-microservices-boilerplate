using FluentValidation;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public class LoginCommandValidator : AbstractValidator<LoginCommand>
{
    public LoginCommandValidator()
    {
        RuleFor(user => user.Dto.Email)
            .NotEmpty();

        RuleFor(user => user.Dto.Password)
            .NotEmpty();
    }
}