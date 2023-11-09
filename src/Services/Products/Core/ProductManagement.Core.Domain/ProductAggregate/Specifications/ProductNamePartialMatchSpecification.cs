using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using ProductManagement.Core.Domain.ProductAggregate.Entities;

namespace ProductManagement.Core.Domain.ProductAggregate.Specifications;

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