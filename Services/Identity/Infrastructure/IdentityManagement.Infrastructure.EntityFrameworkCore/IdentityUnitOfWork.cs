using BuildingBlock.Core.Domain.Shared.Services;

namespace IdentityManagement.Infrastructure.EntityFrameworkCore;

public class IdentityUnitOfWork : IUnitOfWork
{
    private readonly AppDbContext _dbContext;

    public IdentityUnitOfWork(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.CommitAsync();
    }
}