namespace TinyCRM.Core;

public interface IUnitOfWork
{
    Task<int> CommitAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}