using MediatR;

namespace BuildingBlock.Core.Application.CQRS;

public interface IQuery<out TResponse> : IRequest<TResponse>
{
}