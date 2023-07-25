namespace TinyCRM.Infrastructure.UnitOfWork
{
    public interface IUnitOfWork
    {
        Task<int> CommitAsync();
    }
}