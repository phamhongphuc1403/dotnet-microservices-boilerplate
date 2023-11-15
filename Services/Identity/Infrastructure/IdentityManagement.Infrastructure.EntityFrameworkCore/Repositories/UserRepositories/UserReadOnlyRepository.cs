using AutoMapper;
using BuildingBlock.Core.Domain.Repositories;
using IdentityManagement.Core.Domain.UserAggregate.Entities;
using IdentityManagement.Core.Domain.UserAggregate.Repositories;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Entities;
using IdentityManagement.Infrastructure.Identity.UserAggregate.Specifications;
using Microsoft.AspNetCore.Identity;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore.Repositories.UserRepositories;

public class UserReadOnlyRepository : IUserReadOnlyRepository
{
    private readonly IMapper _mapper;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly IReadOnlyRepository<ApplicationUser> _userReadOnlyRepository;

    public UserReadOnlyRepository(IReadOnlyRepository<ApplicationUser> userReadOnlyRepository, IMapper mapper,
        UserManager<ApplicationUser> userManager)
    {
        _userReadOnlyRepository = userReadOnlyRepository;
        _mapper = mapper;
        _userManager = userManager;
    }


    public async Task<User?> GetByIdAsync(Guid id, string? includeTables = null)
    {
        var userIdSpecification = new UserIdSpecification(id);

        var applicationUser = await _userReadOnlyRepository.GetAnyAsync(userIdSpecification, includeTables);

        return _mapper.Map<User?>(applicationUser);
    }

    public async Task<User?> GetByEmailAsync(string email, string? includeTables = null)
    {
        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(email);

        var applicationUser =
            await _userReadOnlyRepository.GetAnyAsync(userEmailExactMatchSpecification, includeTables);

        return _mapper.Map<User?>(applicationUser);
    }

    public async Task<(IEnumerable<User> users, int totalCount)> FilterAndPagingUsers(string keyword, string sort,
        int pageIndex, int pageSize)
    {
        var specification = new UserEmailPartialMatchSpecification(keyword);

        var (applicationUsers, totalCount) = await _userReadOnlyRepository.GetFilterAndPagingAsync(specification, sort,
            pageIndex, pageSize);

        var user = _mapper.Map<IEnumerable<User>>(applicationUsers);

        return (user, totalCount);
    }

    public Task<bool> CheckIfEmailExistAsync(string email)
    {
        var userEmailExactMatchSpecification = new UserEmailExactMatchSpecification(email);

        return _userReadOnlyRepository.CheckIfExistAsync(userEmailExactMatchSpecification);
    }

    public Task<bool> CheckPasswordAsync(User user, string password)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        return _userManager.CheckPasswordAsync(applicationUser, password);
    }

    public Task<string> GetPasswordResetToken(User user)
    {
        var applicationUser = _mapper.Map<ApplicationUser>(user);

        return _userManager.GeneratePasswordResetTokenAsync(applicationUser);
    }
}