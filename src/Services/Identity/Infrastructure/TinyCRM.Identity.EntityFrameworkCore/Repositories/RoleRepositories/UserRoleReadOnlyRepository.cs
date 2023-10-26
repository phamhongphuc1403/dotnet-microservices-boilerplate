using AutoMapper;
using BuildingBlock.Domain.Repositories;
using TinyCRM.Identity.Domain.RoleAggregate.Entities;
using TinyCRM.Identity.Domain.RoleAggregate.Repositories;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Entities;
using TinyCRM.Identity.IdentityDomain.RoleAggregate.Specifications;

namespace TinyCRM.Identity.EntityFrameworkCore.Repositories.RoleRepositories;

public class UserRoleReadOnlyRepository : IUserRoleReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly IReadOnlyRepository<ApplicationUserRole> _userRoleReadOnlyRepository;

    public UserRoleReadOnlyRepository(IReadOnlyRepository<ApplicationUserRole> userRoleReadOnlyRepository,
        IMapper mapper)
    {
        _userRoleReadOnlyRepository = userRoleReadOnlyRepository;
        _mapper = mapper;
    }

    public async Task<UserRole?> GetByUserIdAndRoleIdAsync(Guid userId, Guid roleId)
    {
        var userRoleUserIdSpecification = new UserRoleUserIdSpecification(userId);
        var userRoleRoleIdSpecification = new UserRoleRoleIdSpecification(roleId);

        var specification = userRoleUserIdSpecification.And(userRoleRoleIdSpecification);

        var applicationUserRoles = await _userRoleReadOnlyRepository.GetAnyAsync(specification);

        return _mapper.Map<UserRole?>(applicationUserRoles);
    }
}