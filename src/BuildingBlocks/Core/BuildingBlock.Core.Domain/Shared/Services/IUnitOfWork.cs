namespace BuildingBlock.Core.Domain.Shared.Services;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}