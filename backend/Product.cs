namespace Backend
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Brand { get; set; } = string.Empty;
        public string Sku { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public int StockQuantity { get; set; }
        public string Status { get; set; } = string.Empty;
        public double Rating { get; set; }
        public string Colors { get; set; } = string.Empty; // Comma-separated
        public string Sizes { get; set; } = string.Empty;  // Comma-separated
    }
}