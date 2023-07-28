using Microsoft.EntityFrameworkCore.Storage;

namespace TinyCRM.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
        Task BeginTransactionAsync();
        Task CommitTransactionAsync();
        Task RollbackTransactionAsync();
    }
}