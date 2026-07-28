using OrderService.Models;

namespace OrderService.BLL
{
    public interface IOrderService 
    { 
        Task<List<Order>> GetAllAsync(); 
        Task<Order?> GetByIdAsync(int id); 
        Task<Order> CreateAsync(Order order); 
        Task<Order> UpdateAsync(Order order); 
        Task<bool> DeleteAsync(int id);
        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<Order?> UpdateOrderStatusAsync(int id,string status);
    }
}
