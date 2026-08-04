using NotificationService.Models;

namespace NotificationService.DAL
{
    public interface INotificationRepository
    {
        Task<int> SaveAsync(Notification notification);
        Task<List<Notification>> GetByCustomerIdAsync(int customerId);
    }
}
