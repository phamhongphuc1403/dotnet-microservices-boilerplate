using FluentValidation;
using TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Requests;
using TinyCRM.Identity.Domain.Constants;

namespace TinyCRM.Identity.Application.CQRS.Commands.UserCommands.Validators;

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