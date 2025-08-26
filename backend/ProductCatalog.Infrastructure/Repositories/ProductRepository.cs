using Microsoft.EntityFrameworkCore;
using ProductCatalog.Core.Entities;
using ProductCatalog.Core.Interfaces;
using ProductCatalog.Infrastructure.Data;

namespace ProductCatalog.Infrastructure.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly AppDbContext _context;
        public ProductRepository(AppDbContext context) => _context = context;

        public async Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize, string? sortBy, string? sortDirection)
        {
            var query = _context.Products.AsQueryable();
            // Add sorting logic here if needed
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<int> GetTotalCountAsync() => await _context.Products.CountAsync();

        public async Task<IEnumerable<Product>> SearchProductsAsync(string search, int page, int pageSize, string? sortBy, string? sortDirection)
        {
            var query = _context.Products
                .Where(p => p.Name.Contains(search) || p.Description.Contains(search) || p.Category.Contains(search) || p.Brand.Contains(search) || p.SKU.Contains(search));
            // Add sorting logic here if needed
            return await query.Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();
        }

        public async Task<Product?> GetByIdAsync(int id) => await _context.Products.FindAsync(id);

        public async Task<int> GenerateProductsAsync(int count)
        {
            var products = new List<Product>();
            for (int i = 0; i < count; i++)
            {
                products.Add(new Product
                {
                    Name = $"Product {i + 1}",
                    Description = $"Description for product {i + 1}",
                    Category = "Category",
                    Brand = "Brand",
                    SKU = $"SKU{i + 1}",
                    Price = 10M + i,
                    StockQuantity = 100,
                    Status = AvailabilityStatus.InStock,
                    Rating = 4.5M,
                    Colors = new[] { "Red", "Blue" },
                    Sizes = new[] { "M", "L" }
                });
            }
            _context.Products.AddRange(products);
            return await _context.SaveChangesAsync();
        }
    }
}