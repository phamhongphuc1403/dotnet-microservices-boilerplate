using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using ProductManagement.Core.Domain.ProductAggregate.Entities;
using ProductManagement.Core.Domain.ProductAggregate.Entities.Enums;

namespace ProductManagement.Core.Domain.ProductAggregate.Specifications;

public class ProductTypeSpecification : Specification<Product>
{
    private readonly ProductType? _type;

    public ProductTypeSpecification(ProductType? type)
    {
        _type = type;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (_type == null) return product => true;

        return product => product.Type == _type;
    }
}