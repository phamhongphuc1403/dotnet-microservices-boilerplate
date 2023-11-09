using FluentValidation;
using IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Requests;
using Identitymanagement.Core.Domain.Constants;

namespace IdentityManagement.Core.Application.CQRS.Commands.UserCommands.Validators;

public class ChangeCurrentPasswordCommandValidator : AbstractValidator<ChangeCurrentPasswordCommand>
{
    public ChangeCurrentPasswordCommandValidator()
    {
        RuleFor(x => x.Dto.NewPassword)
            .NotEmpty()
            .Matches(Regex.Password)
            .WithMessage(
                "New password must has the minimum of eight characters, at least one uppercase letter and one number");
    }
}