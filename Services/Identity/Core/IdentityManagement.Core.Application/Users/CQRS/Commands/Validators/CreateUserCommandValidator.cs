using FluentValidation;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(command => command.User.Email)
            .CheckEmailValidation();

        RuleFor(command => command.Password)
            .CheckPasswordValidation()
            .When(command => !string.IsNullOrEmpty(command.Password));

        RuleFor(command => command.User.PhoneNumber)
            .CheckPhoneNumberValidation()
            .When(command => !string.IsNullOrEmpty(command.User.PhoneNumber));
    }
}