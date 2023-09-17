using MediatR;

namespace BuildingBlock.Application.CQRS;

public interface IQueryHandler<in TQuery, TResponse> : IRequestHandler<TQuery, TResponse> where TQuery : IQuery<TResponse>
{
    
}