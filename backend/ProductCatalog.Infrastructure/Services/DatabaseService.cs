using Microsoft.EntityFrameworkCore;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.Infrastructure.Services;

public class DatabaseService
{
    private readonly AppDbContext _context;

    public DatabaseService(AppDbContext context)
    {
        _context = context;
    }

    public async Task MigrateAsync()
    {
        await _context.Database.MigrateAsync();
    }

    public async Task EnsureDatabaseCreatedAsync()
    {
        await _context.Database.EnsureCreatedAsync();
    }
}