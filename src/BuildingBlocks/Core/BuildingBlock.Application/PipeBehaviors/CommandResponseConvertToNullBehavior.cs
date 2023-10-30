using BuildingBlock.Application.CQRS;
using MediatR;

namespace BuildingBlock.Application.PipeBehaviors;

public class CommandResponseConvertToNullBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand<TResponse>
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        PipeBehaviorExtension.ConvertToNull(request);

        return await next();
    }
}

public class CommandConvertToNullBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : ICommand
{
    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        PipeBehaviorExtension.ConvertToNull(request);

        return await next();
    }
}