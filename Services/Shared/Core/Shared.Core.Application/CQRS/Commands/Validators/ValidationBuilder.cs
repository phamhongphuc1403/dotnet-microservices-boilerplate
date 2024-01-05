using FluentValidation;
using Microsoft.AspNetCore.Http;

namespace Shared.Core.Application.CQRS.Commands.Validators;

public static class ValidationBuilder
{
    public static IRuleBuilderOptions<T, IEnumerable<IFormFile>> CheckIfImageQuantityIsLessThan<T>(
        this IRuleBuilder<T, IEnumerable<IFormFile>> ruleBuilder, int quantity)
    {
        return ruleBuilder
            .Must(images => images.Count() <= quantity);
    }

    public static IRuleBuilderOptions<T, IFormFile> IsAnImage<T>(this IRuleBuilder<T, IFormFile> ruleBuilder)
    {
        return ruleBuilder
            .Must(file => file.ContentType.StartsWith("image/"))
            .WithMessage("Only image files are allowed.")
            .When(file => file != null);
    }

    public static IRuleBuilderOptions<T, IFormFile> CheckIfImageSizeIsSmallerThanInMB<T>(
        this IRuleBuilder<T, IFormFile> ruleBuilder,
        int size)
    {
        return ruleBuilder
            .Must(file => file.Length < size * 1024 * 1024)
            .WithMessage($"Image size must be less than {size}MB.")
            .When(file => file != null);
    }
}