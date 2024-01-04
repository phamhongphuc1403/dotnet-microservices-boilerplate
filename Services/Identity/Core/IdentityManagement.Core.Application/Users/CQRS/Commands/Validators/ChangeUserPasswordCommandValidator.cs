using FluentValidation;
using IdentityManagement.Core.Application.Users.CQRS.Commands.Requests;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public class ChangeUserPasswordCommandValidator : AbstractValidator<ChangeUserPasswordCommand>
{
    public ChangeUserPasswordCommandValidator()
    {
        RuleFor(x => x.Dto.Password)
            .CheckPasswordValidation();
    }
}