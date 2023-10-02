using MediatR;

namespace BuildingBlock.Application.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}