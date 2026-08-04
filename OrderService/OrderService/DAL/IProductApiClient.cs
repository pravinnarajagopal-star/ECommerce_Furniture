using OrderService.Models;

namespace OrderService.DAL
{
    public interface IProductApiClient
    {
        Task<ProductDto?> GetProductAsync(int productId);
        Task<ProductDto?> UpdateProductAsync(ProductDto product);
    }
}
