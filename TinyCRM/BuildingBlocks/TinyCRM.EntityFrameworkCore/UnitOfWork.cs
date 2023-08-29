using Microsoft.EntityFrameworkCore.Storage;
using TinyCRM.Core;

namespace TinyCRM.EntityFrameworkCore;

public class UnitOfWork : IUnitOfWork
{
    private readonly DbFactory _dbFactory;
    private IDbContextTransaction _transaction = null!;

    public UnitOfWork(DbFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }

    public Task<int> CommitAsync()
    {
        return _dbFactory.DbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        _transaction = await _dbFactory.DbContext.Database.BeginTransactionAsync();
    }

    public async Task CommitTransactionAsync()
    {
        await _transaction.CommitAsync();
    }

    public async Task RollbackTransactionAsync()
    {
        await _transaction.RollbackAsync();
    }
}