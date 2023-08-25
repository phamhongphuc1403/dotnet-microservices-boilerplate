using MediatR;

namespace TinyCRM.Core.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}