using OrderService.Models;

namespace OrderService.DAL
{
    public interface IProductApiClient
    {
        Task<ProductDto?> GetProductAsync(int productId);
    }
}
