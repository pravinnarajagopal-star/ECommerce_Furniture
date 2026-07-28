using Microsoft.EntityFrameworkCore;
using PaymentService.Models;

namespace PaymentService.DAL
{
    public class PaymentRepository: IPaymentRepository
    {
        private readonly AppDbContext _context;
        public PaymentRepository(AppDbContext context) 
        { 
            _context = context; 
        }

        public async Task<List<Payment>> GetAllAsync() 
        { 
            return await _context.Payments.AsNoTracking().ToListAsync(); 
        }

        public async Task<Payment?> GetByIdAsync(int paymentId) 
        {
            return await _context.Payments.FirstOrDefaultAsync(x => x.PaymentId == paymentId); 
        }
        public async Task<Payment?> GetByOrderIdAsync(int orderId) 
        { 
            return await _context.Payments.FirstOrDefaultAsync(x => x.OrderId == orderId); 
        }
        public async Task<Payment> AddAsync(Payment payment) 
        { 
            _context.Payments.Add(payment); 
            await _context.SaveChangesAsync(); 
            return payment; 
        }
        public async Task<Payment> UpdateAsync(Payment payment) 
        {
            _context.Payments.Update(payment); 
            await _context.SaveChangesAsync(); 
            return payment; 
        }
        public async Task<bool> DeleteAsync(int paymentId) 
        {
            var payment = await _context.Payments.FindAsync(paymentId);
            if (payment == null) return false; 
            _context.Payments.Remove(payment); 
            await _context.SaveChangesAsync();
            return true; 
        }


        public async Task<IEnumerable<Payment>> GetCompletedPaymentsAsync()
        {
            return await _context.Payments
                .Where(x => x.PaymentStatus == "Completed")
                .ToListAsync();
        }

    }
}
