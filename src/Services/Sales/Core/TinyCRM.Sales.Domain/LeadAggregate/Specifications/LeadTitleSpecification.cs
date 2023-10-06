using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications.Abstractions;
using TinyCRM.Sales.Domain.LeadAggregate.Entities;

namespace TinyCRM.Sales.Domain.LeadAggregate.Specifications;

public class LeadTitleSpecification : Specification<Lead>, ISpecification<Lead>
{
    private readonly string _title;

    public LeadTitleSpecification(string title)
    {
        _title = title;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        if (string.IsNullOrWhiteSpace(_title)) return lead => true;

        return lead => lead.Title.ToUpper().Contains(_title.ToUpper());
    }
}