namespace BuildingBlock.Core.Application;

public interface IDataSeeder
{
    int ExecutionOrder { get; }
    Task SeedDataAsync();
}