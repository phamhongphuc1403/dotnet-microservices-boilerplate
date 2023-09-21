using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Products.Domain.Entities;
using TinyCRM.Products.Domain.Entities.Enums;

namespace TinyCRM.Products.Domain.Specifications;

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