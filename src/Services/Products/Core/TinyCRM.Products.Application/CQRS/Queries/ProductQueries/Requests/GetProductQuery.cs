using BuildingBlock.Application.CQRS;
using TinyCRM.Products.Application.DTOs;

namespace TinyCRM.Products.Application.CQRS.Queries.ProductQueries.Requests;

public record GetProductQuery(Guid ProductId) : IQuery<ProductDetailDto>
{
}