using OrderService.Models;

namespace OrderService.DAL
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetAllAsync(); 
        Task<Order?> GetByIdAsync(int id);
        Task<Order> AddAsync(Order order); 
        Task<Order> UpdateAsync(Order order);
        Task<bool> DeleteAsync(int id);

        Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId);

        Task<Order?> UpdateStatusAsync(int id,string status);
    }
}
