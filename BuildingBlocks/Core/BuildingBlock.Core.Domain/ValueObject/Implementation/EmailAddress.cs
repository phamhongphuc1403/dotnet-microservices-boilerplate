using BuildingBlock.Core.Domain.ValueObject.Implementation.Validators;
using FluentValidation;

namespace BuildingBlock.Core.Domain.ValueObject.Implementation;

public sealed class EmailAddress : Abstractions.ValueObject
{
    public EmailAddress(string value)
    {
        Value = value;
        ValidateValues();
    }

    public string Value { get; }

    public override IEnumerable<object?> GetValues()
    {
        yield return Value;
    }

    protected override void ValidateValues()
    {
        var validator = new EmailAddressValidator();

        validator.ValidateAndThrow(this);
    }
}