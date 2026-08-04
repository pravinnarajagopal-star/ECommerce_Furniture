using NotificationService.DAL;
using NotificationService.Models;

namespace NotificationService.BLL
{
    public class NotificationService : INotificationService
    {
        private readonly INotificationRepository _repository;

        public NotificationService(INotificationRepository repository)
        {
            _repository = repository;
        }

        public async Task<int> SendEmailAsync(Notification notification)
        {
            notification.EmailStatus = "Sent";
            notification.CreatedDate = DateTime.Now;

            return await _repository.SaveAsync(notification);
        }

        public async Task<int> SendSmsAsync(Notification notification)
        {
            notification.SMSStatus = "Sent";
            notification.CreatedDate = DateTime.Now;

            return await _repository.SaveAsync(notification);
        }

        public async Task<List<Notification>> GetHistoryAsync(int customerId)
        {
            return await _repository.GetByCustomerIdAsync(customerId);
        }
    }
}
