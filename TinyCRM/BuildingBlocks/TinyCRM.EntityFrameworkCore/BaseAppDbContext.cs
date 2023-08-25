using Microsoft.EntityFrameworkCore;

namespace TinyCRM.EntityFrameworkCore;

public class BaseAppDbContext : DbContext
{
    protected BaseAppDbContext(DbContextOptions options) : base(options)
    {
    }
}