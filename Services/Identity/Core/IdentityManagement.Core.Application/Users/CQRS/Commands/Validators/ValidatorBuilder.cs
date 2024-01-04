using FluentValidation;

namespace IdentityManagement.Core.Application.Users.CQRS.Commands.Validators;

public static class ValidatorBuilder
{
    public static IRuleBuilderOptions<T, string> CheckEmailValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .EmailAddress()
            .MaximumLength(320);
    }

    public static IRuleBuilderOptions<T, string> CheckPasswordValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage(
                "Password must has the minimum of eight characters, at least one uppercase letter and one number.");
    }

    public static IRuleBuilderOptions<T, string> CheckPhoneNumberValidation<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .Matches(@"^(84|0)[35789]\d{8}$")
            .WithMessage("'{PropertyValue}' is not a valid phone number.");
    }
}