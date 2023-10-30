using BuildingBlock.Application.CQRS;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;

namespace BuildingBlock.Application.PipeBehaviors;

public class CommandResponseValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    private readonly ILogger<IValidator<TRequest>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public CommandResponseValidationBehavior(IEnumerable<IValidator<TRequest>> validators,
        ILogger<IValidator<TRequest>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var failures = PipeBehaviorExtension.GetFailures(request, _validators);

        if (!failures.Any()) return await next();

        _logger.LogWarning("Validation errors - {RequestType} - Request: {@Request} - Errors: {@ValidationErrors}",
            typeof(TRequest).Name, request, failures);

        throw new ValidationException(failures);
    }
}

public class CommandValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand
{
    private readonly ILogger<IValidator<TRequest>> _logger;
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public CommandValidationBehavior(IEnumerable<IValidator<TRequest>> validators, ILogger<IValidator<TRequest>> logger)
    {
        _validators = validators;
        _logger = logger;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();

        var failures = PipeBehaviorExtension.GetFailures(request, _validators);

        if (!failures.Any()) return await next();

        _logger.LogWarning("Validation errors - {RequestType} - Request: {@Request} - Errors: {@ValidationErrors}",
            typeof(TRequest).Name, request, failures);

        throw new ValidationException(failures);
    }
}