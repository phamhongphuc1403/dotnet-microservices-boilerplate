using FluentValidation;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using IdentityManagement.Core.Domain.Constants;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Validators;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.Dto.Password)
            .NotEmpty()
            .Matches(Regex.Password)
            .WithMessage(
                "Password must has the minimum of eight characters, at least one uppercase letter and one number");
    }
}