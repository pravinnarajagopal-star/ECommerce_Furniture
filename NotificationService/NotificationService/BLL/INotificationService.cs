using NotificationService.Models;

namespace NotificationService.BLL
{
    public interface INotificationService
    {
        Task<int> SendEmailAsync(Notification notification);
        Task<int> SendSmsAsync(Notification notification);
        Task<List<Notification>> GetHistoryAsync(int customerId);
    }
}
