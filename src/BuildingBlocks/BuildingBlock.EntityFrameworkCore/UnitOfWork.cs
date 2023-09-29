using BuildingBlock.Domain;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace BuildingBlock.EntityFrameworkCore;

public class UnitOfWork<TDbContext> : IUnitOfWork where TDbContext : DbContext
{
    private readonly TDbContext _dbContext;
    private IDbContextTransaction? _transaction;

    public UnitOfWork(TDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public Task<int> SaveChangesAsync()
    {
        return _dbContext.SaveChangesAsync();
    }

    public async Task BeginTransactionAsync()
    {
        if (_transaction == null)
            _transaction = await _dbContext.Database.BeginTransactionAsync();
        else
            throw new InvalidOperationException("Transaction already started.");
    }

    public async Task CommitTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
        else
        {
            throw new InvalidOperationException("Transaction not started.");
        }
    }

    public async Task RollbackTransactionAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
            await _transaction.DisposeAsync();
            _transaction = null;
        }
        else
        {
            throw new InvalidOperationException("Transaction not started.");
        }
    }
}