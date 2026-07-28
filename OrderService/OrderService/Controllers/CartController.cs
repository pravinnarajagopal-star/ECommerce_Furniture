using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.BLL;
using OrderService.Models;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService) 
        { 
            _cartService = cartService;
        }


        // GET: api/cart/1
        [HttpGet("{customerId}")] 
        public async Task<IActionResult> GetCart(int customerId) 
        { 
            var cart = await _cartService.GetCartAsync(customerId);
            if (cart == null) return NotFound();
            return Ok(cart); 
        }


        // POST: api/cart
        [HttpPost] 
        public async Task<IActionResult> CreateCart([FromBody] Cart cart) 
        { 
            var result = await _cartService.CreateCartAsync(cart);
            return Ok(result); 
        }



        // POST: api/cart/add-item
        [HttpPost("add-item")] 
        public async Task<IActionResult> AddItem([FromBody] CartItem item) 
        { 
            var result = await _cartService.AddItemAsync(item); 
            return Ok(result); 
        }



        // DELETE: api/cart/remove-item/5
        [HttpDelete("remove-item/{cartItemId}")] 
        public async Task<IActionResult> RemoveItem(int cartItemId) 
        { 
            var deleted = await _cartService.RemoveItemAsync(cartItemId);
            if (!deleted) return NotFound(); 
            return Ok(new { Message = "Cart item removed successfully." }); 
        }
    }
}
