using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using SaleManagement.Core.Domain.DealAggregate.Entities;
using SaleManagement.Core.Domain.DealAggregate.Entities.Enums;

namespace SaleManagement.Core.Domain.DealAggregate.Specifications;

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