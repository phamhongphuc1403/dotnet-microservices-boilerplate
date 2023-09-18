using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Domain.Specifications;

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
        
        return lead => lead.Title.Contains(_title);
    }
}