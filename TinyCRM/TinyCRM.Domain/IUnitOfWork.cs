namespace TinyCRM.Domain
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();

        Task BeginTransactionAsync();

        Task CommitTransactionAsync();

        Task RollbackTransactionAsync();
    }
}