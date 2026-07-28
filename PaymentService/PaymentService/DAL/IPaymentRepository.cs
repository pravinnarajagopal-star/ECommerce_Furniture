using PaymentService.Models;

namespace PaymentService.DAL
{
    public interface IPaymentRepository
    {
        Task<List<Payment>> GetAllAsync(); 
        Task<Payment?> GetByIdAsync(int paymentId); 
        Task<Payment?> GetByOrderIdAsync(int orderId); 
        Task<Payment> AddAsync(Payment payment); 
        Task<Payment> UpdateAsync(Payment payment);
        Task<bool> DeleteAsync(int paymentId);

        Task<IEnumerable<Payment>> GetCompletedPaymentsAsync();

    }
}
