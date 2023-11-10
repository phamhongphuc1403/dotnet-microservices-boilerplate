using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using SaleManagement.Core.Domain.LeadAggregate.Entities;
using SaleManagement.Core.Domain.LeadAggregate.Entities.Enums;

namespace SaleManagement.Core.Domain.LeadAggregate.Specifications;

public class LeadStatusSpecification : Specification<Lead>, ISpecification<Lead>
{
    private readonly LeadStatus? _status;

    public LeadStatusSpecification(LeadStatus? status)
    {
        _status = status;
    }

    public override Expression<Func<Lead, bool>> ToExpression()
    {
        if (_status == null) return lead => true;

        return lead => lead.Status == _status;
    }
}