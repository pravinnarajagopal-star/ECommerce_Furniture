using NotificationService.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace NotificationService.DAL
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly AppDbContext _context;

        public NotificationRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<int> SaveAsync(Notification notification)
        {
            _context.Notifications.Add(notification);
            await _context.SaveChangesAsync();

            return notification.NotificationId;
        }

        public async Task<List<Notification>> GetByCustomerIdAsync(int customerId)
        {
            return await _context.Notifications
                .Where(x => x.CustomerId == customerId)
                .ToListAsync();
        }
    }
}
