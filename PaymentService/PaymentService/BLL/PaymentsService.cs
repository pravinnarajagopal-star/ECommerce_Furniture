using PaymentService.DAL;
using PaymentService.Models;

namespace PaymentService.BLL
{
    public class PaymentsService : IPaymentService
    {
        private readonly IPaymentRepository _repository; public PaymentsService(IPaymentRepository repository) { _repository = repository; }
        public async Task<List<Payment>> GetAllAsync() { return await _repository.GetAllAsync(); }
        public async Task<Payment?> GetByIdAsync(int paymentId) { return await _repository.GetByIdAsync(paymentId); }
        public async Task<Payment?> GetByOrderIdAsync(int orderId) { return await _repository.GetByOrderIdAsync(orderId); }

        public async Task<Payment> CreatePaymentAsync(Payment payment) { payment.PaymentStatus = "Pending"; payment.PaymentDate = DateTime.UtcNow; payment.CreatedDate = DateTime.UtcNow; return await _repository.AddAsync(payment); }

        public async Task<Payment?> UpdatePaymentAsync(Payment payment)
        {
            var existingPayment = await _repository.GetByIdAsync(payment.PaymentId);
            if (existingPayment == null) return null; 
            existingPayment.PaymentMethod = payment.PaymentMethod; 
            existingPayment.Amount = payment.Amount;
            existingPayment.PaymentStatus = payment.PaymentStatus;
            existingPayment.UpdatedBy = payment.UpdatedBy;
            existingPayment.UpdatedOn = DateTime.UtcNow;
            return await _repository.UpdateAsync(existingPayment);
        }

        public async Task<bool> DeletePaymentAsync(int paymentId) 
        { return await _repository.DeleteAsync(paymentId); }


        public async Task<Payment?> UpdatePaymentStatusAsync(int paymentId, string status, string transactionId)
        {
            var payment = await _repository.GetByIdAsync(paymentId); 
            if (payment == null) return null; 
            payment.PaymentStatus = status; 
            payment.TransactionId = transactionId; 
            payment.UpdatedOn = DateTime.UtcNow;
            return await _repository.UpdateAsync(payment);
        }


        public async Task<Payment?> UpdatePaymentStatusByOrderAsync(int orderId, string status, string transactionId)
        {
            var payment = await _repository.GetByOrderIdAsync(orderId);
            if (payment == null) return null;
            payment.PaymentStatus = status;
            payment.TransactionId = transactionId;
            payment.UpdatedOn = DateTime.UtcNow;
            return await _repository.UpdateAsync(payment);
        }

        public async Task<IEnumerable<Payment>> GetCompletedPaymentsAsync()
        {
            return await _repository
                .GetCompletedPaymentsAsync();
        }


    }
}
