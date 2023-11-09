using FluentValidation;
using TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Requests;

namespace TinyCRM.Products.Application.CQRS.Commands.ProductCommands.Validators;

public class EditProductCommandValidator : AbstractValidator<EditProductCommand>
{
    public EditProductCommandValidator()
    {
        RuleFor(product => product.Dto.Code)
            .NotEmpty()
            .When(product => string.IsNullOrWhiteSpace(product.Dto.Code))
            .MaximumLength(256);

        RuleFor(product => product.Dto.Name)
            .NotEmpty()
            .When(product => string.IsNullOrWhiteSpace(product.Dto.Name))
            .MaximumLength(256);

        RuleFor(product => product.Dto.Price)
            .InclusiveBetween(0, double.MaxValue);
    }
}