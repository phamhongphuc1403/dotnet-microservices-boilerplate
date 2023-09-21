using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Domain.Specifications;

public class DealTitleSpecification : Specification<Deal>, ISpecification<Deal>
{
    private readonly string _title;

    public DealTitleSpecification(string title)
    {
        _title = title;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_title)) return deal => true;

        return deal => deal.Title.Contains(_title);
    }
}