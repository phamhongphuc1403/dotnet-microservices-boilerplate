using System.Linq.Expressions;
using BuildingBlock.Core.Domain.Specifications.Abstractions;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;

namespace IdentityManagement.Infrastructure.Identity.UserAggregate.Specifications;

public class UserPhoneNumberSpecification : Specification<ApplicationUser>
{
    private readonly string _phoneNumber;

    public UserPhoneNumberSpecification(string phoneNumber)
    {
        _phoneNumber = phoneNumber;
    }

    public override Expression<Func<ApplicationUser, bool>> ToExpression()
    {
        return user => user.PhoneNumber == _phoneNumber;
    }
}