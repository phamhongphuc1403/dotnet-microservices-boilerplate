using FluentValidation;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public class ChangeCurrentPasswordCommandValidator : AbstractValidator<ChangeCurrentPasswordCommand>
{
    public ChangeCurrentPasswordCommandValidator()
    {
        RuleFor(x => x.Dto.NewPassword)
            .CheckPasswordValidation();
    }
}