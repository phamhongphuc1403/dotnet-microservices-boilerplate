using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Queries.Requests;

public class GetProductQuery : IQuery<ProductDto>
{
    public Guid Id { get; private set; }
    public GetProductQuery(Guid id)
    {
        Id = id;
    }
}