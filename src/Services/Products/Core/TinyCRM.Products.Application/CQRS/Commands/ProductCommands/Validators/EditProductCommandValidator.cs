using FluentValidation;
using TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Validators;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(product => product.Code)
            .NotEmpty()
            .When(product => string.IsNullOrWhiteSpace(product.Code))
            .MaximumLength(256);

        RuleFor(product => product.Name)
            .NotEmpty()
            .When(product => string.IsNullOrWhiteSpace(product.Name))
            .MaximumLength(256);

        RuleFor(product => product.Price)
            .InclusiveBetween(0, double.MaxValue);
    }
}