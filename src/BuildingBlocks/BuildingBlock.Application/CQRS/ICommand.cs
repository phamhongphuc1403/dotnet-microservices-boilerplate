using MediatR;

namespace BuildingBlock.Application.CQRS;

public interface ICommand : IRequest
{
    
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}