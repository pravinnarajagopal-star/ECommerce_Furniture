using OrderService.DAL;
using OrderService.Models;

namespace OrderService.BLL
{
    public class CartService : ICartService
    {
        private readonly ICartRepository _repository;
        public CartService(ICartRepository repository) 
        { 
            _repository = repository; 
        }
        public async Task<Cart?> GetCartAsync(int customerId) 
        { 
            return await _repository.GetCartByCustomerIdAsync(customerId); 
        }

        public async Task<Cart> CreateCartAsync(Cart cart) 
        { 
            cart.CreatedDate = DateTime.Now; 
            cart.CreatedOn = DateTime.Now; 
            return await _repository.CreateCartAsync(cart); 
        }

        public async Task<Cart> AddItemAsync(CartItem item) 
        { 
            item.CreatedDate = DateTime.Now; 
            return await _repository.AddCartItemAsync(item); 
        }
        public async Task<bool> RemoveItemAsync(int cartItemId) 
        { 
            return await _repository.RemoveCartItemAsync(cartItemId); 
        }
    }
}
