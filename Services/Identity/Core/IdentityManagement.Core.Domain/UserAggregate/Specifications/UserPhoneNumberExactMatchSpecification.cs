using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Core.Domain.UserAggregate.Entities;

namespace IdentityManagement.Core.Domain.UserAggregate.Specifications;

public class UserPhoneNumberExactMatchSpecification : Specification<User>
{
    private readonly string _phoneNumber;

    public UserPhoneNumberExactMatchSpecification(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public override Expression<Func<User, bool>> ToExpression()
    {
        return user => user.PhoneNumber == _phoneNumber;
    }
}