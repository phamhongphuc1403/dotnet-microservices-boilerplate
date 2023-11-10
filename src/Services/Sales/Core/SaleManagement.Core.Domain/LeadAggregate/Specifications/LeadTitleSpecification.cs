using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using SaleManagement.Core.Domain.LeadAggregate.Entities;

namespace SaleManagement.Core.Domain.LeadAggregate.Specifications;

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