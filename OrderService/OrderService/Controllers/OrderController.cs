using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using OrderService.BLL;
using OrderService.Models;
using OrderService.RabbitMQServer;
using OrderService.UserService;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        private readonly OrderPublisher _publisher;
        private readonly IUserServiceClient _userServiceClient;
        public OrderController(IOrderService orderService, OrderPublisher publisher,IUserServiceClient userServiceClient) 
        { 
            _orderService = orderService;
            _publisher = publisher;
            _userServiceClient = userServiceClient;
        }

        // GET: api/orders
        [HttpGet] 
        public async Task<IActionResult> GetAllOrders() 
        { 
            var orders = await _orderService.GetAllAsync();
            return Ok(orders);
        }

        // GET: api/orders/5
        [HttpGet("{id}")] 
        public async Task<IActionResult> GetOrderById(int id) 
        { 
            var order = await _orderService.GetByIdAsync(id); 
            if (order == null) return NotFound();
            return Ok(order); 
        }


        // POST: api/orders
        [HttpPost] 
        public async Task<IActionResult> Post(Order order) 
        { 
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var result = await _orderService.CreateAsync(order);

            // Fetching user details from UserService using the CustomerId

            var user = await _userServiceClient.GetUserAsync(order.CustomerId);

            //calling rabbitMQ publisher to send the order to the queue

            var orderEvent = new OrderCreatedEvent
            {
                OrderId = order.OrderId,
                CustomerId=order.CustomerId,
                Amount = order.TotalAmount,
                Email = user.Email,
                CustomerName = user.FullName
            };
            await _publisher.PublishAsync(orderEvent);

            return CreatedAtAction(nameof(GetOrderById), new { id = result.OrderId }, result); 
        }


        // PUT: api/orders
        [HttpPut] 
        public async Task<IActionResult> UpdateOrder([FromBody] Order order) 
        { 
            if (!ModelState.IsValid) return BadRequest(ModelState);
            var result = await _orderService.UpdateAsync(order);
            return Ok(result);
        }


        // DELETE: api/orders/5
        [HttpDelete("{id}")] 
        public async Task<IActionResult> DeleteOrder(int id) 
        { 
            var deleted = await _orderService.DeleteAsync(id); 
            if (!deleted) return NotFound(); 
            return Ok(new { Message = "Order deleted successfully." }); 
        }


        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = _orderService.GetAllAsync().Result.ToList().Count();
            return Ok(count);
        }


        // GET: api/orders/user/5
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetOrdersByUser(int userId)
        {
            try
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);



                if (orders == null || !orders.Any())
                {
                    return NotFound(new
                    {
                        message = "No orders found for this user"
                    });
                }

                return Ok(new
                {
                    success = true,
                    data = orders
                });
            }

            catch (Exception ex)
            {
                return StatusCode(500, new
                {
                    message = "Error while fetching orders",
                    error = ex.Message
                });
            }
        }


        // PATCH: api/orders/5/status
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(
            int id,
            [FromBody] UpdateOrderStatusRequest request)
        {
            var result = await _orderService.UpdateOrderStatusAsync(
                id,
                request.Status
            );

            if (result == null)
                return NotFound(new { Message = "Order not found" });

            return Ok(result);
        }


        public class UpdateOrderStatusRequest
        {
            public string Status { get; set; } = string.Empty;
        }







    }
}
