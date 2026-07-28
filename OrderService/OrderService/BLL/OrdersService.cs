using OrderService.DAL;
using OrderService.Models;

namespace OrderService.BLL
{
    public class OrdersService : IOrderService
    {
        private readonly IOrderRepository _repository;
        private readonly IProductApiClient _productClient;
        public OrdersService(IOrderRepository repository, IProductApiClient productClient) 
        {
            _repository = repository;
            _productClient = productClient;
        }

        public async Task<List<Order>> GetAllAsync() 
        { 
            return await _repository.GetAllAsync(); 
        }
        public async Task<Order?> GetByIdAsync(int id) 
        { 
            return await _repository.GetByIdAsync(id); 
        }

        public async Task<Order> CreateAsync(Order order)
        { 
            order.OrderDate = DateTime.Now; 
            order.CreatedDate = DateTime.Now; 
            if (order.OrderItems != null && order.OrderItems.Any()) 
            { 
                order.TotalAmount = order.OrderItems.Sum(x => x.Quantity * x.Price); 
                foreach (var item in order.OrderItems) 
                { 
                    item.CreatedDate = DateTime.Now;
                } 
            } return await _repository.AddAsync(order); 
        }


        public async Task<Order> UpdateAsync(Order order) 
        { 
            order.UpdatedOn = DateTime.Now;
            if (order.OrderItems != null && order.OrderItems.Any()) 
            { 
                order.TotalAmount = order.OrderItems.Sum(x => x.Quantity * x.Price);
            } 
            return await _repository.UpdateAsync(order); 
        }


        public async Task<bool> DeleteAsync(int id)
        {
            return await _repository.DeleteAsync(id); 
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _repository.GetOrdersByUserIdAsync(userId);

            var result = new List<Order>();

            foreach (var order in orders)
            {
                var dto = new Order
                {
                    OrderId = order.OrderId,
                    OrderDate = order.OrderDate,
                    OrderStatus = order.OrderStatus,
                    TotalAmount = order.TotalAmount,
                    OrderItems = new List<OrderItem>()
                };

                foreach (var item in order.OrderItems)
                {
                    
                    var product = await _productClient.GetProductAsync(item.ProductId);

                    dto.OrderItems.Add(new OrderItem
                    {
                        ProductId = item.ProductId,
                        ProductName = product?.ProductName ?? "Unknown Product",
                        ImageUrl = product?.ImageUrl ?? string.Empty,
                        Price = item.Price,
                        Quantity = item.Quantity
                    });
                }

                result.Add(dto);
            }
            return result;
        }


        public async Task<Order?> UpdateOrderStatusAsync(int id,string status)
        {

            return await _repository.UpdateStatusAsync(id,status);

        }
    }
}
