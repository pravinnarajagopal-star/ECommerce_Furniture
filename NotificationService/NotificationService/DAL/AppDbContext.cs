using Microsoft.EntityFrameworkCore;
using NotificationService.Models;

namespace NotificationService.DAL
{
    public class AppDbContext: DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Notification> Notifications { get; set; }
    }
}
