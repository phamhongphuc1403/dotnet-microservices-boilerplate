using MediatR;

namespace TinyCRM.Core.CQRS;

public interface ICommand : IRequest
{
    
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
    
}