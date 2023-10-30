using BuildingBlock.Domain.Shared.Services;
using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;

    public UnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }
}