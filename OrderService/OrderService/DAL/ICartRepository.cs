using OrderService.Models;

namespace OrderService.DAL
{
    public interface ICartRepository
    {
        Task<Cart?> GetCartByCustomerIdAsync(int customerId); Task<Cart> CreateCartAsync(Cart cart); Task<Cart> AddCartItemAsync(CartItem item); Task<bool> RemoveCartItemAsync(int cartItemId);
    }
}
