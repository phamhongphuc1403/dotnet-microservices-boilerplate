using System.Linq.Expressions;
using FluentValidation;
using PhoneNumbers;

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

    public static IRuleBuilderOptions<T, string> CheckPasswordValidation<T>(this IRuleBuilder<T, string> ruleBuilder,
        Expression<Func<T, string>> confirmPassword)
    {
        return ruleBuilder
            .NotEmpty()
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage(
                "Password must has the minimum of eight characters, at least one uppercase letter and one number.")
            .Equal(confirmPassword)
            .WithMessage("Password must match the confirmed password.");
    }

    public static IRuleBuilderOptions<T, string?> CheckPasswordValidation<T>(this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$")
            .WithMessage(
                "Password must has the minimum of eight characters, at least one uppercase letter and one number.");
    }

    public static IRuleBuilderOptions<T, string?> CheckPhoneNumberValidation<T>(
        this IRuleBuilder<T, string?> ruleBuilder)
    {
        return ruleBuilder
            .Must(phoneNumber =>
            {
                var phoneNumberUtil = PhoneNumberUtil.GetInstance();

                try
                {
                    phoneNumberUtil.Parse(phoneNumber, null);
                    return true;
                }
                catch (Exception _)
                {
                    return false;
                }
            })
            .WithMessage("'{PropertyValue}' is not a valid phone number.");
    }

    public static IRuleBuilderOptions<T, Guid> CheckIdValidation<T>(this IRuleBuilder<T, Guid> ruleBuilder)
    {
        return ruleBuilder
            .NotEmpty()
            .NotEqual(new Guid())
            .WithMessage("'{PropertyValue}' is not a valid id.");
    }
}