using FluentValidation;
using Shared.Core.Application.CQRS.Commands.Requests;

namespace Shared.Core.Application.CQRS.Commands.Validators;

public class UploadImageCommandValidator : AbstractValidator<UploadImageCommand>
{
    public UploadImageCommandValidator()
    {
        RuleFor(command => command.Images)
            .NotEmpty()
            .CheckIfImageQuantityIsLessThan(10);

        RuleForEach(command => command.Images)
            .IsAnImage()
            .CheckIfImageSizeIsSmallerThanInMB(5);
    }
}