using MediatR;

namespace BuildingBlock.Core.CQRS;

public interface ICommand : IRequest
{
    
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}