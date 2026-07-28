namespace ProductService.Models;

public class Product
{
    public int ProductId { get; set; }

    public required string ProductName { get; set; }

    public required string Description { get; set; }

    public required string Category { get; set; }

    public decimal Price { get; set; }

    public int Quantity { get; set; }

    public string? ImageUrl { get; set; }

    public decimal Rating { get; set; }

    public bool StockAvailable { get; set; }

    public DateTime CreatedDate { get; set; }

    public string? CreatedBy { get; set; }

    public string? UpdatedBy { get; set; }

    public DateTime? UpdatedOn { get; set; }
}
