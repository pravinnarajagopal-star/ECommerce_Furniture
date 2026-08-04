namespace OrderService.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;

        public int Quantity { get; set; } = 0;

        public required string Description { get; set; } = string.Empty;

        public required string Category { get; set; } = string.Empty;

        public decimal Price { get; set; }
        public decimal Rating { get; set; }

        public bool StockAvailable { get; set; }
    }
}
