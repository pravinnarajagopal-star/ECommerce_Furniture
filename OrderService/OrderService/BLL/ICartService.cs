using OrderService.Models;

namespace OrderService.BLL
{
    public interface ICartService
    {
        Task<Cart?> GetCartAsync(int customerId); 
        Task<Cart> CreateCartAsync(Cart cart);
        Task<Cart> AddItemAsync(CartItem item); 
        Task<bool> RemoveItemAsync(int cartItemId);
    }
}
