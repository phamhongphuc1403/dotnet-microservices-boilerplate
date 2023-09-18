using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Sales.Domain.Entities;

namespace TinyCRM.Sales.Domain.Specifications;

public class LeadIdSpecification: Specification<Lead>, ISpecification<Lead>
{
    private readonly Guid _id;

    public LeadIdSpecification(Guid id)
    {
        _id = id;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        return lead => lead.Id == _id;
    }
}