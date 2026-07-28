using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PaymentService.BLL;
using PaymentService.Models;

namespace PaymentService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentsController : ControllerBase
    {
        private readonly IPaymentService _paymentService; 
        public PaymentsController(IPaymentService paymentService)
        { 
            _paymentService = paymentService; 
        }


        // GET: api/payments
        [HttpGet] 
        public async Task<IActionResult> GetAll() 
        {
            var payments = await _paymentService.GetAllAsync();
            return Ok(payments); 
        }


        // GET: api/payments/5
         [HttpGet("{id:int}")] 
        public async Task<IActionResult> GetById(int id)
        {
            var payment = await _paymentService.GetByIdAsync(id);
            if (payment == null) return NotFound(new { Message = "Payment not found" });
            return Ok(payment); 
        }

        // GET: api/payments/order/1001
        [HttpGet("order/{orderId:int}")]
        public async Task<IActionResult> GetByOrderId( int orderId) 
        { 
            var payment = await _paymentService.GetByOrderIdAsync(orderId);
            if (payment == null) return NotFound(new { Message = "Payment not found for order" });
            return Ok(payment); 
        }


        // POST: api/payments
        [HttpPost] 
        public async Task<IActionResult> Create( [FromBody] Payment payment) 
        {
            if (!ModelState.IsValid) return BadRequest(ModelState); 
            var result = await _paymentService.CreatePaymentAsync(payment);
            return CreatedAtAction( nameof(GetById), new { id = result.PaymentId }, result); 
        }


        // PUT: api/payments
        [HttpPut] 
        public async Task<IActionResult> Update( [FromBody] Payment payment) 
        { 
            var result = await _paymentService.UpdatePaymentAsync(payment);
            if (result == null) return NotFound(new { Message = "Payment not found" });
            return Ok(result); 
        }

        // PATCH: api/payments/5/status
        [HttpPatch("{id:int}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdatePaymentStatusRequest request) 
        {
            var result = await _paymentService.UpdatePaymentStatusByOrderAsync(id, request.Status, request.TransactionId); if (result == null) return NotFound(new { Message = "Payment not found" });
            return Ok(result); 
        }


        // DELETE: api/payments/5
        [HttpDelete("{id:int}")] public async Task<IActionResult> Delete(int id) 
        {
            var deleted = await _paymentService.DeletePaymentAsync(id);
            if (!deleted) return NotFound(new { Message = "Payment not found" }); 
            return Ok(new { Message = "Payment deleted successfully" }); 
        }

        public class UpdatePaymentStatusRequest 
        { 
            public string Status { get; set; } = string.Empty; 
            public string TransactionId { get; set; } = string.Empty;
        }

        [HttpGet("count")]
        public async Task<IActionResult> GetCount()
        {
            var count = _paymentService.GetCompletedPaymentsAsync().Result.ToList().Count();
            return Ok(count);
        }
    }
}
