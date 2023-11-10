using BuildingBlock.Core.Application.CQRS;
using ProductManagement.Core.Application.DTOs;

namespace ProductManagement.Core.Application.CQRS.Queries.ProductQueries.Requests;

public record GetProductQuery(Guid ProductId) : IQuery<ProductDetailDto>
{
}