using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class BaseDbContext : DbContext
{
    protected BaseDbContext(DbContextOptions options) : base(options)
    {
    }
}