using FluentValidation;
using FluentValidation.Results;

namespace BuildingBlock.Core.Application.PipeBehaviors;

public static class PipeBehaviorExtension
{
    public static void ConvertToNull<TRequest>(TRequest request)
    {
        var properties = typeof(TRequest).GetProperties();

        foreach (var property in properties)
        {
            if (property.PropertyType != typeof(string)) continue;

            var value = (string)property.GetValue(request)!;

            if (string.IsNullOrWhiteSpace(value)) property.SetValue(request, null);
        }
    }

    public static List<ValidationFailure> GetFailures<TRequest>(TRequest request,
        IEnumerable<IValidator<TRequest>> validators)
    {
        return validators
            .Select(v => v.Validate(request))
            .SelectMany(result => result.Errors)
            .Where(error => error != null)
            .ToList();
    }
}