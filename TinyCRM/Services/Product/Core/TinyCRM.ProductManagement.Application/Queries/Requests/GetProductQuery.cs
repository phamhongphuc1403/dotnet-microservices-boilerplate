using BuildingBlock.Core.CQRS;
using TinyCRM.ProductManagement.Application.DTOs;

namespace TinyCRM.ProductManagement.Application.Queries.Requests;

public class GetProductQuery : IQuery<ProductDto>
{
    public Guid Id { get; private set; }
    public GetProductQuery(Guid id)
    {
        Id = id;
    }
}