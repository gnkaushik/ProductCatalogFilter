using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Infrastructure.Data;
using ProductCatalog.Infrastructure.Repositories;
using Xunit;

namespace ProductCatalog.Tests.Repositories;

public class ProductRepositoryTests
{
    private readonly DbContextOptions<AppDbContext> _options;

    public ProductRepositoryTests()
    {
        _options = new DbContextOptionsBuilder<AppDbContext>()
            .UseInMemoryDatabase(databaseName: System.Guid.NewGuid().ToString())
            .Options;
    }

    private ProductRepository GetRepository()
    {
        var context = new AppDbContext(_options);
        return new ProductRepository(context);
    }

    private AppDbContext GetContext()
    {
        return new AppDbContext(_options);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsEmpty_WhenNoProducts()
    {
        var repo = GetRepository();
        var result = await repo.GetProductsAsync(1, 10, null, null);
        Assert.Empty(result);
    }

    [Fact]
    public async Task GetProductsAsync_ReturnsPagedResults()
    {
        var context = GetContext();
        context.Products.AddRange(
            new Product { Name = "A", SKU = "SKU1", Price = 10 },
            new Product { Name = "B", SKU = "SKU2", Price = 20 }
        );
        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);
        var result = await repo.GetProductsAsync(1, 1, null, null);

        Assert.Single(result);
    }

    [Fact]
    public async Task SearchProductsAsync_ReturnsMatchingProducts()
    {
        var context = GetContext();
        context.Products.AddRange(
            new Product { Name = "Alpha", SKU = "SKU1", Price = 10 },
            new Product { Name = "Beta", SKU = "SKU2", Price = 20 }
        );
        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);
        var result = await repo.SearchProductsAsync("Alpha", 1, 10, null, null);

        Assert.Single(result);
        Assert.Equal("Alpha", result.First().Name);
    }

    [Fact]
    public async Task SearchProductsAsync_ReturnsEmpty_WhenNoMatch()
    {
        var context = GetContext();
        context.Products.Add(new Product { Name = "Gamma", SKU = "SKU3", Price = 30 });
        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);
        var result = await repo.SearchProductsAsync("NonExistent", 1, 10, null, null);

        Assert.Empty(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsNull_WhenNotFound()
    {
        var repo = GetRepository();
        var result = await repo.GetByIdAsync(999);
        Assert.Null(result);
    }

    [Fact]
    public async Task GetByIdAsync_ReturnsProduct_WhenExists()
    {
        var context = GetContext();
        var product = new Product { Name = "Delta", SKU = "SKU4", Price = 40 };
        context.Products.Add(product);
        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);
        var result = await repo.GetByIdAsync(product.Id);

        Assert.NotNull(result);
        Assert.Equal("Delta", result.Name);
    }

    [Fact]
    public async Task GenerateProductsAsync_AddsCorrectNumberOfProducts()
    {
        var repo = GetRepository();
        var count = await repo.GenerateProductsAsync(5);

        Assert.Equal(5, count);
    }

    [Fact]
    public async Task GetTotalCountAsync_ReturnsCorrectCount()
    {
        var context = GetContext();
        context.Products.AddRange(
            new Product { Name = "Epsilon", SKU = "SKU5", Price = 50 },
            new Product { Name = "Zeta", SKU = "SKU6", Price = 60 }
        );
        await context.SaveChangesAsync();

        var repo = new ProductRepository(context);
        var total = await repo.GetTotalCountAsync();

        Assert.Equal(2, total);
    }
}