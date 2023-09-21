using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Products.Domain.Entities;

namespace TinyCRM.Products.Domain.Specifications;

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

        return product => product.Name.Contains(_name);
    }
}