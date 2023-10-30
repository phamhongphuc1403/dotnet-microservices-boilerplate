namespace BuildingBlock.Domain.Shared.Services;

public interface IUnitOfWork
{
    Task<int> SaveChangesAsync();
}