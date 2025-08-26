using ProductCatalog.Core.Entities;

namespace ProductCatalog.Core.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<Product>> GetProductsAsync(int page, int pageSize, string? sortBy, string? sortDirection);
        Task<int> GetTotalCountAsync();
        Task<IEnumerable<Product>> SearchProductsAsync(string query, int page, int pageSize, string? sortBy, string? sortDirection);
        Task<Product?> GetByIdAsync(int id);
        Task<int> GenerateProductsAsync(int count);
    }
}