using FluentValidation;
using ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Requests;

namespace ProductManagement.Core.Application.CQRS.Commands.ProductCommands.Validators;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
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