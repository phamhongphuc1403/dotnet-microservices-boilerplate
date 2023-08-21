using Microsoft.EntityFrameworkCore;
using TinyCRM.EntityFrameworkCore.Data;

namespace TinyCRM.EntityFrameworkCore;

public class DbFactory : IDisposable
{
    private readonly Func<AppDbContext> _instanceFunc;
    private DbContext? _dbContext;
    private bool _disposed;

    public DbFactory(Func<AppDbContext> dbContextFactory)
    {
        _instanceFunc = dbContextFactory;
    }

    public DbContext DbContext => _dbContext ??= _instanceFunc.Invoke();

    public void Dispose()
    {
        if (!_disposed && _dbContext != null)
        {
            _disposed = true;
            _dbContext.Dispose();
        }
    }
}