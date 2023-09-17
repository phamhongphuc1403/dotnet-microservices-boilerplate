namespace BuildingBlock.Core;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();

    Task BeginTransactionAsync();

    Task CommitTransactionAsync();

    Task RollbackTransactionAsync();
}