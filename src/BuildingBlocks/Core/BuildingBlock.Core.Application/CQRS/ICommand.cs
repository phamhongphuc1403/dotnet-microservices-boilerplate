using MediatR;

namespace BuildingBlock.Core.Application.CQRS;

public interface ICommand : IRequest
{
}

public interface ICommand<out TResponse> : IRequest<TResponse>
{
}