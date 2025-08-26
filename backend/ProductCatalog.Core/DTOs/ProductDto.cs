namespace ProductCatalog.Core.DTOs;

public class ProductDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Category { get; set; } = string.Empty;
    public string Brand { get; set; } = string.Empty;
    public string SKU { get; set; } = string.Empty;
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public string Status { get; set; } = string.Empty;
    public decimal Rating { get; set; }
    public string[] Colors { get; set; } = Array.Empty<string>();
    public string[] Sizes { get; set; } = Array.Empty<string>();
}