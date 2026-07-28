using Microsoft.EntityFrameworkCore;
using OrderService.Models;

namespace OrderService.DAL
{
    public class OrderRepository: IOrderRepository
    {
        private readonly AppDbContext _context;
        public OrderRepository(AppDbContext context) { _context = context; }

        public async Task<List<Order>> GetAllAsync() { return await _context.Orders.Include(x => x.OrderItems).ToListAsync(); }
        public async Task<Order?> GetByIdAsync(int id) { return await _context.Orders.Include(x => x.OrderItems).FirstOrDefaultAsync(x => x.OrderId == id); }
        public async Task<Order> AddAsync(Order order) { _context.Orders.Add(order); await _context.SaveChangesAsync(); return order; }

        public async Task<Order> UpdateAsync(Order order) { _context.Orders.Update(order); await _context.SaveChangesAsync(); return order; }
        public async Task<bool> DeleteAsync(int id) 
        { 
            var order = await _context.Orders.FindAsync(id); 
            if (order == null) 
                return false; 
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync(); 
            return true; 
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserIdAsync(int userId)
        {
            return await _context.Orders
                .Include(o => o.OrderItems)
                .Where(o => o.CustomerId == userId)
                .OrderByDescending(o => o.OrderDate)
                .ToListAsync();
        }


        public async Task<Order?> UpdateStatusAsync(int id, string status)
        {

            var order = await _context.Orders
                .FirstOrDefaultAsync( x => x.OrderId == id);


            if (order == null)
                return null;


            order.OrderStatus = status;


            await _context.SaveChangesAsync();


            return order;

        }

    }
}
