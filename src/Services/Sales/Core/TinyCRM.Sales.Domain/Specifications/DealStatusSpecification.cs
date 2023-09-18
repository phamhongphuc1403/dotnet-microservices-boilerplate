using System.Linq.Expressions;
using BuildingBlock.Domain.Specifications;
using TinyCRM.Sales.Domain.Entities;
using TinyCRM.Sales.Domain.Entities.Enums;

namespace TinyCRM.Sales.Domain.Specifications;

public class DealStatusSpecification : Specification<Deal>, ISpecification<Deal>
{
    private readonly DealStatus? _status;

    public DealStatusSpecification(DealStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<Deal, bool>> ToExpression()
    {
        if (_status == null) return lead => true;

        return deal => deal.Status == _status;
    }
}