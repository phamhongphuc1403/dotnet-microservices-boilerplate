using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;

public class GetProductQuery : IQuery<ProductDetailDto>
{
    public GetProductQuery(Guid id)
    {
        Id = id;
    }

    public Guid Id { get; }
}