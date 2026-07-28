namespace OrderService.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; } = string.Empty;

        public string ImageUrl { get; set; } = string.Empty;
    }
}
