using Microsoft.EntityFrameworkCore;

namespace BuildingBlock.EntityFrameworkCore;

public class BaseAppDbContext : DbContext
{
    protected BaseAppDbContext(DbContextOptions options) : base(options)
    {
    }
}