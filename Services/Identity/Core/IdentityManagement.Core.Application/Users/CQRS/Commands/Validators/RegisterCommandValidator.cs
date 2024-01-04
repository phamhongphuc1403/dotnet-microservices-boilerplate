using FluentValidation;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public class RegisterCommandValidator : AbstractValidator<RegisterCommand>
{
    public RegisterCommandValidator()
    {
        RuleFor(command => command.Dto.Email)
            .CheckEmailValidation();

        RuleFor(command => command.Dto.Password)
            .CheckPasswordValidation();
    }
}