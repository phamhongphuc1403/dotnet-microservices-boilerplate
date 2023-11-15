using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.RoleAggregate.Entities;
using IdentityManagement.Core.Domain.RoleAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.RoleAggregate.Specifications;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.RoleRepositories;

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