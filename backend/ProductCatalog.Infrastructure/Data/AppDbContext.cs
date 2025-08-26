using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;

namespace ProductCatalog.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<Product> Products => Set<Product>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.Id);
            
            entity.Property(p => p.Colors)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));

            entity.Property(p => p.Sizes)
                .HasConversion(
                    v => string.Join(',', v),
                    v => v.Split(',', StringSplitOptions.RemoveEmptyEntries));
                    
            entity.Property(p => p.Name).IsRequired().HasMaxLength(200);
            entity.Property(p => p.SKU).IsRequired().HasMaxLength(50);
            entity.Property(p => p.Price).HasPrecision(18, 2);
        });
    }
}