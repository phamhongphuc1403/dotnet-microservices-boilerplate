using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Products.Domain.ProductAggregate.Entities;

namespace TinyCRM.Products.Domain.ProductAggregate.Specifications;

public class ProductNamePartialMatchSpecification : Specification<Product>
{
    private readonly string _name;

    public ProductNamePartialMatchSpecification(string name)
    {
        _name = name;
    }

    public override Expression<Func<Product, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_name)) return product => true;

        return product => product.Name.ToUpper().Contains(_name.ToUpper());
    }
}