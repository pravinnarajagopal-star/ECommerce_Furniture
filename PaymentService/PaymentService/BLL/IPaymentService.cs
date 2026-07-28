using PaymentService.Models;

namespace PaymentService.BLL
{
    public interface IPaymentService 
    {

        Task<List<Payment>> GetAllAsync(); 
        Task<Payment?> GetByIdAsync(int paymentId); 
        Task<Payment?> GetByOrderIdAsync(int orderId); 
        Task<Payment> CreatePaymentAsync(Payment payment); 
        Task<Payment?> UpdatePaymentAsync(Payment payment);
        Task<bool> DeletePaymentAsync(int paymentId); 
        Task<Payment?> UpdatePaymentStatusAsync(int paymentId, string status, string transactionId);

        Task<Payment?> UpdatePaymentStatusByOrderAsync(int orderId, string status, string transactionId);

        Task<IEnumerable<Payment>> GetCompletedPaymentsAsync();



    }
}
