using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Domain.Constants;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Validators;

public class ChangeCurrentPasswordCommandValidator : AbstractValidator<ChangeCurrentPasswordCommand>
{
    public ChangeCurrentPasswordCommandValidator()
    {
        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .Matches(Regex.Password)
            .WithMessage(
                "New password must has the minimum of eight characters, at least one uppercase letter and one number");
    }
}