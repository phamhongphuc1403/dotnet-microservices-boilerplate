using FluentValidation;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.Dto.Email)
            .CheckEmailValidation();

        RuleFor(command => command.Dto.Password)
            .CheckPasswordValidation(command => command.Dto.ConfirmPassword);
    }
}