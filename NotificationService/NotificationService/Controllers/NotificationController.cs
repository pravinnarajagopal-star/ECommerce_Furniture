using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NotificationService.BLL;
using NotificationService.Models;

namespace NotificationService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotificationController : ControllerBase
    {
        private readonly INotificationService _service;

        public NotificationController(INotificationService service)
        {
            _service = service;
        }

        [HttpPost("email")]
        public async Task<IActionResult> SendEmail(Notification notification)
        {
            var id = await _service.SendEmailAsync(notification);

            return Ok(new
            {
                NotificationId = id,
                Message = "Email sent successfully"
            });
        }


        [HttpPost("sms")]
        public async Task<IActionResult> SendSms(Notification notification)
        {
            var id = await _service.SendSmsAsync(notification);

            return Ok(new
            {
                NotificationId = id,
                Message = "SMS sent successfully"
            });
        }


        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetHistory(int customerId)
        {
            var result = await _service.GetHistoryAsync(customerId);

            return Ok(result);
        }
    }
}
