using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using SaleManagement.Core.Domain.DealAggregate.Entities;

namespace SaleManagement.Core.Domain.DealAggregate.Specifications;

public class DealTitleSpecification : Specification<Deal>
{
    private readonly string _title;

    public DealTitleSpecification(string title)
    {
        _title = title;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_title)) return deal => true;

        return deal => deal.Title.ToUpper().Contains(_title.ToUpper());
    }
}