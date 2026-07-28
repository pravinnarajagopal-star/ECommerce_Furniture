using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.DAL
{
    public class CartRepository : ICartRepository
    {
        private readonly AppDbContext _context; 
        public CartRepository(AppDbContext context) 
        { 
            _context = context; 
        }

        public async Task<Cart?> GetCartByCustomerIdAsync(int customerId)
        { 
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CustomerId == customerId);
        }
        public async Task<Cart> CreateCartAsync(Cart cart) 
        { _context.Carts.Add(cart); 
            await _context.SaveChangesAsync(); 
            return cart; 
        }

        public async Task<Cart> AddCartItemAsync(CartItem item)
        { 
            _context.CartItems.Add(item); 
            await _context.SaveChangesAsync(); 
            return await _context.Carts
                .Include(c => c.CartItems)
                .FirstOrDefaultAsync(c => c.CartId == item.CartId); 
        }
        public async Task<bool> RemoveCartItemAsync(int cartItemId) 
        { 
            var item = await _context.CartItems
                .FindAsync(cartItemId); 
            if (item == null) 
                return false; 
            _context.CartItems.Remove(item); 
            await _context.SaveChangesAsync(); 
            return true; 
        }
    }
}
