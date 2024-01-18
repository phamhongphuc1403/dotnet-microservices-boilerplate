using FluentValidation;

namespace BuildingBlock.Core.Domain.ValueObject.Implementation.Validators;

public class EmailAddressValidator : AbstractValidator<EmailAddress>
{
    public EmailAddressValidator()
    {
        RuleFor(emailAddress => emailAddress.Value)
            .NotEmpty()
            .EmailAddress();
    }
}