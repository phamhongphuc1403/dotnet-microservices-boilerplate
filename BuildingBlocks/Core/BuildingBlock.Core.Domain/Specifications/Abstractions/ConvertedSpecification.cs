using System.Linq.Expressions;
using AutoMapper;

namespace BuildingBlock.Core.Domain.Specifications.Abstractions;

public class ConvertedSpecification<TFirst, TSecond> : Specification<TSecond> where TFirst : IEntity
    where TSecond : IEntity
{
    private readonly IMapper _mapper;
    private readonly Specification<TFirst> _specification;

    public ConvertedSpecification(Specification<TFirst> specification, IMapper mapper)
    {
        _specification = specification;
        _mapper = mapper;
    }

    public override Expression<Func<TSecond, bool>> ToExpression()
    {
        return _mapper.Map<Expression<Func<TSecond, bool>>>(_specification.ToExpression());
    }
}