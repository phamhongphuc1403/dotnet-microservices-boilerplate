using MediatR;

namespace BuildingBlock.Core.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
    
}